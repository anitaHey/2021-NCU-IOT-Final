package com.anita.iotfinalzenbo;

import static android.Manifest.permission.INTERNET;
import static android.Manifest.permission.RECORD_AUDIO;

import static com.asus.robotframework.API.RobotFace.CONFIDENT;
import static com.asus.robotframework.API.RobotFace.DOUBTING;
import static com.asus.robotframework.API.RobotFace.EXPECTING;
import static com.asus.robotframework.API.RobotFace.HAPPY;
import static com.asus.robotframework.API.RobotFace.IMPATIENT;
import static com.asus.robotframework.API.RobotFace.INTERESTED;
import static com.asus.robotframework.API.RobotFace.LAZY;
import static com.asus.robotframework.API.RobotFace.QUESTIONING;
import static com.asus.robotframework.API.RobotFace.SHOCKED;
import static com.asus.robotframework.API.RobotFace.INNOCENT;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;

import android.content.Intent;
import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.asus.robotframework.API.RobotCallback;
import com.asus.robotframework.API.RobotCmdState;
import com.asus.robotframework.API.RobotCommand;
import com.asus.robotframework.API.RobotErrorCode;
import com.asus.robotframework.API.DialogSystem;

import com.asus.robotframework.API.Utility;
import com.microsoft.cognitiveservices.speech.CancellationDetails;
import com.microsoft.cognitiveservices.speech.CancellationReason;
import com.microsoft.cognitiveservices.speech.PropertyId;
import com.microsoft.cognitiveservices.speech.ResultReason;
import com.microsoft.cognitiveservices.speech.SpeechConfig;
import com.microsoft.cognitiveservices.speech.intent.*;

import org.json.JSONArray;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.Socket;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import java.util.concurrent.Semaphore;

public class MainActivity extends RobotActivity {

    public MainActivity() {
        super(robotCallback, robotListenCallback);
    }
    // Replace below with your LUIS subscription key
    private static String speechSubscriptionKey = "0394886ff6dc4510a1afcf8d9d80dd65";
    // Replace below with your LUIS service region (e.g., "westus").
    private static String serviceRegion = "westus";
    // Replace below with your LUIS Application ID.
    private static String appId = "cab1c012-1613-447c-b128-81dc7eba79e4";

    public static final int TYPE_CAPACITY_TOUCH = Utility.SensorType.CAPACITY_TOUCH;
    private SensorManager mSensorManager;
    private Sensor mSensorCapacityTouch;

    private SpeechConfig speechConfig;
    private IntentRecognizer reco;

    /*Zenbo info*/
    boolean busy = false;
    int current_zenbo = 0;
    /*Internet info*/
    public String ip = "";
    public int port = 6000;
    //Socket info
    private Socket socket;
    private DataOutputStream InstructionSender;
    private BufferedReader InstructionReader;

    /*MainActivity element*/
    EditText ip_input;
    EditText port_input;
    Button btn_connect;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        InitActivityElement();
        /*
        while(!socket.isConnected()){
            //wait for socket connect
        }
        */

        //robotAPI.robot.setExpression(SHOCKED ,"連接Server成功 Zenbo 這就為您服務");
        mSensorManager = (SensorManager)getSystemService(SENSOR_SERVICE);
        // sensors
        mSensorCapacityTouch = mSensorManager.getDefaultSensor(TYPE_CAPACITY_TOUCH);
        // Note: we need to request the permissions
        int requestCode = 5; // unique code for the permission request
        ActivityCompat.requestPermissions(MainActivity.this, new String[]{RECORD_AUDIO, INTERNET}, requestCode);

        // Initialize speech synthesizer and its dependencies
        speechConfig = SpeechConfig.fromSubscription(speechSubscriptionKey, serviceRegion);
        speechConfig.setSpeechRecognitionLanguage("zh-TW");
        assert(speechConfig != null);
        reco = new IntentRecognizer(speechConfig);
        assert(reco != null);

        // Creates a language understanding model using the app id, and adds specific intents from your model
        LanguageUnderstandingModel model = LanguageUnderstandingModel.fromAppId(appId);
        reco.addIntent(model, "None");
        reco.addIntent(model, "點餐");
        reco.addIntent(model, "好評");
        reco.addIntent(model, "客訴");
        reco.addIntent(model, "帶位");
        reco.addIntent(model, "送客");

        btn_connect.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                ip = ip_input.getText().toString();
                port = Integer.parseInt(port_input.getText().toString());

                Log.d("connect_server", "Click");
                Log.d("connect_server", ip + Integer.toString(port));
                server_connect();
            }
        });

    }
    @Override
    protected void onResume() {
        super.onResume();
        mSensorManager.registerListener(listenerCapacityTouch, mSensorCapacityTouch, SensorManager.SENSOR_DELAY_UI);
    }

    @Override
    protected void onPause() {
        super.onPause();
        robotAPI.robot.unregisterListenCallback();robotAPI.robot.unregisterListenCallback();
        mSensorManager.unregisterListener(listenerCapacityTouch);

    }

    public static RobotCallback robotCallback = new RobotCallback() {
        @Override
        public void onResult(int cmd, int serial, RobotErrorCode err_code, Bundle result) {
            super.onResult(cmd, serial, err_code, result);

            Log.d("RobotDevSample", "onResult:"
                    + RobotCommand.getRobotCommand(cmd).name()
                    + ", serial:" + serial + ", err_code:" + err_code
                    + ", result:" + result.getString("RESULT"));
        }

        @Override
        public void onStateChange(int cmd, int serial, RobotErrorCode err_code, RobotCmdState state) {
            super.onStateChange(cmd, serial, err_code, state);
        }

        @Override
        public void initComplete() {
            super.initComplete();

        }
    };


    public static RobotCallback.Listen robotListenCallback = new RobotCallback.Listen() {
        @Override
        public void onFinishRegister() {
        }
        @Override
        public void onVoiceDetect(JSONObject jsonObject) {
        }
        @Override
        public void onSpeakComplete(String s, String s1) {
        }
        @Override
        public void onEventUserUtterance(JSONObject jsonObject) {
        }
        @Override
        public void onResult(JSONObject jsonObject) {
        }
        @Override
        public void onRetry(JSONObject jsonObject) {
        }
    };

    public void Zenbo_LUIS() {
        robotAPI.robot.setExpression(EXPECTING ,"正在聽你講話\n");
        try {
            // Note: this will block the UI thread, so eventually, you want to register for the event
            Future<IntentRecognitionResult> task = reco.recognizeOnceAsync();
            assert(task != null);
            IntentRecognitionResult result = task.get();
            assert(result != null);
            String res = "";

            // Checks result.
            if (result.getReason() == ResultReason.RecognizedIntent) {
                res = res.concat("RECOGNIZED: Text=" + result.getText());
                String responseStr = result.getProperties().getProperty(PropertyId.LanguageUnderstandingServiceResponse_JsonResult);
                try {
                    JSONObject response = new JSONObject(responseStr);
                    JSONArray entities = response.getJSONArray("entities");
                    JSONObject entity = entities.getJSONObject(0);
                    String entityStr = entity.getString("entity");
                    res = res.concat("RECOGNIZED: entity=" + entityStr);
                }catch (Exception e){
                    Log.e("JSON", "exception", e);
                }
                switch(result.getIntentId()){
                    case "帶位" :
                        //!!
                        int customer_number = 0;
                        send_find_table(customer_number);
                        res = res.concat("Zenbo 正在搜索空桌\n");
                        robotAPI.robot.setExpression(CONFIDENT ,res);
                        break;
                    case "點餐" :
                        //!!
                        int dish_id = 0;
                        send_order(current_zenbo,dish_id);
                        res = res.concat("好的，這就為您準備\n");
                        robotAPI.robot.setExpression(EXPECTING ,res);
                        break;
                    case "好評" :
                        res = res.concat("感謝您的回覆\n");
                        robotAPI.robot.setExpression(INTERESTED ,res);
                        break;
                    case "客訴" :
                        res = res.concat("Zenbo 覺得不開心\n");
                        robotAPI.robot.setExpression(IMPATIENT ,res);
                        break;
                    case "送客" :
                        res = res.concat("Zenbo 覺得不開心\n");
                        robotAPI.robot.setExpression(INNOCENT ,res);
                        break;
                    case "None" :
                        res = res.concat("麻煩您再說一遍\n");
                        robotAPI.robot.setExpression(DOUBTING ,res);
                        break;
                }
                res = res.concat("    Intent Id: " + result.getIntentId());
                res = res.concat("    Intent Service JSON: " + result.getProperties().getProperty(PropertyId.LanguageUnderstandingServiceResponse_JsonResult));
            }
            else if (result.getReason() == ResultReason.RecognizedSpeech) {
                res = res.concat("RECOGNIZED: Text=" + result.getText());
                res = res.concat("Zenbo 聽不懂\n");
                robotAPI.robot.setExpression(INNOCENT , res);
                res = res.concat("RECOGNIZED: Text=" + result.getText());
                res = res.concat("Intent not recognized.");

            }
            else if (result.getReason() == ResultReason.NoMatch) {
                res = res.concat("NOMATCH: Speech could not be recognized.");
                robotAPI.robot.setExpression(INNOCENT , "Zenbo 甚麼都沒聽到");
            }
            else if (result.getReason() == ResultReason.Canceled) {
                CancellationDetails cancellation = CancellationDetails.fromResult(result);
                res = res.concat("CANCELED: Reason=" + cancellation.getReason());

                if (cancellation.getReason() == CancellationReason.Error) {
                    res = res.concat("CANCELED: ErrorCode=" + cancellation.getErrorCode());
                    res = res.concat("CANCELED: ErrorDetails=" + cancellation.getErrorDetails());
                    res = res.concat("CANCELED: Did you update the subscription info?");
                }
                Log.w("LUIS", res);
            }
            result.close();
        } catch (Exception ex) {
            Log.e("SpeechSDKDemo", "unexpected " + ex.getMessage());
            assert(false);
        }
    }
    SensorEventListener listenerCapacityTouch = new SensorEventListener() {
        @Override
        public void onSensorChanged(SensorEvent event) {
            Zenbo_LUIS();
        }
        @Override
        public void onAccuracyChanged(Sensor sensor, int accuracy) {

        }
    };

    /*Function for connect to server*/
    private void InitActivityElement(){
        btn_connect = (Button) findViewById(R.id.btn_connect);
        ip_input = (EditText) findViewById(R.id.ip_input);
        port_input = (EditText) findViewById(R.id.port_input);
    }
    private void server_connect(){
        Thread socket_thread = new Thread(new Runnable() {
            @Override
            public void run() {
                try{
                    socket = new Socket(ip, port);
                    Log.d("connect_server", "Try Connect ");
                    if(socket.isConnected()){
                        Log.d("connect_server", "Connect Success");
                        InstructionSender = new DataOutputStream(socket.getOutputStream());
                        Thread receive = new Thread(ReceiveInstruction);
                        receive .start();
                    }
                } catch (Exception e){
                    Log.e("connect_server", "exception", e);
                }
            }
        });
        socket_thread.start();
    }
    private Runnable ReceiveInstruction = new Runnable() {
        @Override
        public void run() {
            Log.d("connect_server", "ReceiveThread start Success");
            while(socket.isConnected()){
                try{
                    InstructionReader = new BufferedReader(new InputStreamReader(socket.getInputStream()));
                    String InstructionStr = InstructionReader.readLine();
                    JSONObject Instruction = new JSONObject(InstructionStr);

                    //Analyze Instruction Element
                    String Instruction_type = Instruction.getString("type");
                    int table_id = Instruction.getInt("table_id");
                    double x = Instruction.getDouble("x");
                    double y = Instruction.getDouble("y");
                    //Implement Instruction
                    busy = true;
                    switch(Instruction_type){
                        case "response_table":
                            response_table(table_id, x, y);
                            break;
                        case "response_service":
                            response_service(table_id, x, y);
                            break;
                        case "response_leave":
                            response_leave(table_id, x, y);
                            break;
                    }
                    busy = false;

                }catch (Exception e) {
                    Log.e("connect_server", "exception", e);
                }
            }
        }
    };

    /*Function after Zenbo get instruction*/
    void response_table( int table_id, double x, double y){
        String sentence = "Zenbo 這就為您帶位 目的地" + table_id + "號桌";
        robotAPI.robot.setExpression(CONFIDENT, sentence);
        Log.d("Zenbo_Say", sentence);
        //!!go to table destination
        robotAPI.motion.moveBody((float)x, (float)y, (int)0.0);
        current_zenbo = table_id;
    }
    void response_service( int table_id, double x, double y){
        //!!go to table destination
        robotAPI.motion.moveBody((float)x, (float)y, (int)0.0);
        current_zenbo = table_id;
        robotAPI.robot.setExpression(QUESTIONING, "有什麼是 Zenbo 能為您服務的嗎");
        Log.d("Zenbo_Say", "有什麼是 Zenbo 能為您服務的嗎");

    }
    void response_leave( int table_id, double x, double y){
        //!!go to table destination
        robotAPI.motion.moveBody((float)x, (float)y, (int)0.0);
        current_zenbo = table_id;
        robotAPI.robot.setExpression(INNOCENT ,"請跟Zenbo前往出口");
        Log.d("Zenbo_Say", "請跟Zenbo前往出口");
        //!!go to door
        robotAPI.motion.moveBody((float)x, (float)y, (int)0.0);
        current_zenbo = 0;

        robotAPI.robot.setExpression(INNOCENT ,"請慢走");
        Log.d("Zenbo_Say", "請慢走");
    }

    /*Function send info to server*/
    void send_find_table(int customer_number){
        try {
            JSONObject msg = new JSONObject();
            msg.put("type", "find_table");
            msg.put("customer_number", customer_number);
            ZenboSend(msg);
        }catch (Exception e){
            Log.e("Socket Fail",e.toString());
        }
    }
    void send_order(int current_zenbo, int dish_id){
        try {
            JSONObject msg = new JSONObject();
            msg.put("type", "order");
            msg.put("table_id", current_zenbo);
            msg.put("meals_id", dish_id);
            ZenboSend(msg);
        }catch (Exception e){
            Log.e("Socket Fail",e.toString());
        }
    }
    private void ZenboSend(JSONObject msg){
        Thread clientThread = new Thread(new Runnable()
        {
            @Override
            public void run() {
                try {
                    robotAPI.robot.setExpression(CONFIDENT, "Zenbo 正在回傳訊息");
                    InstructionSender.write((msg.toString() + "\n").getBytes());
                    InstructionSender.flush();
                }catch (Exception e) {
                    Log.e("Socket Fail",e.toString());
                }
            }
        });
        clientThread.start();
    }

}