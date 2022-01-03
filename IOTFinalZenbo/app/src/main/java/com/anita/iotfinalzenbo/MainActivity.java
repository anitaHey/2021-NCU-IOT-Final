package com.anita.iotfinalzenbo;

import static android.Manifest.permission.INTERNET;
import static android.Manifest.permission.RECORD_AUDIO;

import static com.asus.robotframework.API.RobotFace.HAPPY;
import static com.asus.robotframework.API.RobotFace.LAZY;
import static com.asus.robotframework.API.RobotFace.SHOCKED;
import static com.asus.robotframework.API.RobotFace.INNOCENT;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.app.ActivityCompat;

import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
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

import org.json.JSONObject;

import java.util.concurrent.ExecutionException;
import java.util.concurrent.Future;
import java.util.concurrent.Semaphore;

public class MainActivity extends RobotActivity {

    public MainActivity() {
        super(robotCallback, robotListenCallback);
    }
    // Replace below with your LUIS subscription key
    private static String speechSubscriptionKey = "9723e9d2d7f841d085f265ed34563f2e";
    // Replace below with your LUIS service region (e.g., "westus").
    private static String serviceRegion = "westus";
    // Replace below with your LUIS Application ID.
    private static String appId = "05271267-c31e-4ba0-908b-e277b5118087";

    public static final int TYPE_CAPACITY_TOUCH = Utility.SensorType.CAPACITY_TOUCH;
    private SensorManager mSensorManager;
    private Sensor mSensorCapacityTouch;

    private SpeechConfig speechConfig;
    private IntentRecognizer reco;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //setContentView(R.layout.activity_main);

        robotAPI.robot.setExpression(SHOCKED);

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
        reco.addIntent(model, "評價");

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
        robotAPI.robot.setExpression(HAPPY ,"正在聽你講話\n");
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
                switch(result.getIntentId()){
                    case "點餐" :
                        res = res.concat("好的，這就為您準備\n");
                        break;
                    case "評價" :
                        res = res.concat("感謝您的回覆\n");
                        break;
                    case "None" :
                        res = res.concat("麻煩您再說一遍\n");
                        break;
                }
                robotAPI.robot.setExpression(HAPPY ,res);
                res = res.concat("    Intent Id: " + result.getIntentId());
                res = res.concat("    Intent Service JSON: " + result.getProperties().getProperty(PropertyId.LanguageUnderstandingServiceResponse_JsonResult));
            }
            else if (result.getReason() == ResultReason.RecognizedSpeech) {
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

}