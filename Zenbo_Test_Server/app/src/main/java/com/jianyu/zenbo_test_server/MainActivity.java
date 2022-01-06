package com.jianyu.zenbo_test_server;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.os.Handler;
import android.os.Message;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.Inet4Address;
import java.net.InetAddress;
import java.net.NetworkInterface;
import java.net.ServerSocket;
import java.net.Socket;
import java.net.SocketException;
import java.util.Enumeration;

public class MainActivity extends AppCompatActivity {

    /*Internet info*/
    public String host = "192.168.1.113";
    public int port = 7000;
    //socket
    private ServerSocket serverSocket;
    private Socket client;
    private BufferedReader InstructionReader;
    private DataOutputStream InstructionSender;
    /*Activity Element*/
    Button btn_send;
    EditText instruction_type;
    EditText table_id;
    EditText x;
    EditText y;
    TextView ReceiveMessage;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        InitViewElement();
        ServerStart();

        btn_send.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                try {
                    ServerSend(InstructionSender);
                    /*
                    JSONObject msg = new JSONObject();
                    msg.put("type", instruction_type.getText().toString());
                    msg.put("table_id", table_id.getText().toString());
                    msg.put("x", table_id.getText().toString());
                    msg.put("y", table_id.getText().toString());
                    InstructionSender.write((msg.toString() + "\n").getBytes());
                    InstructionSender.flush();
                    */
                }catch (Exception e){
                    Log.e("Socket Fail",e.toString());
                }
            }
        });

    }

    private void InitViewElement(){
        btn_send = (Button) findViewById(R.id.btn_send);
        instruction_type = (EditText) findViewById(R.id.instruction_type);
        table_id = (EditText) findViewById(R.id.table_id);
        x = (EditText) findViewById(R.id.x);
        y = (EditText) findViewById(R.id.y);
        ReceiveMessage = (TextView) findViewById(R.id.ReceiveMessage);
    }

    private void ServerStart(){
        try{
            serverSocket = new ServerSocket(port);
            ReceiveMessage.setText(getLocalIpAddress());
            Log.d("Socket","Server Create");
            ServerListen();
            Log.d("Socket","Start Listen");
        }
        catch (Exception e) {
            Log.e("Socket Fail",e.toString());
        }
    }

    private void ServerListen(){
        Thread clientThread = new Thread(new Runnable()
        {
            @Override
            public void run() {
                try {
                    while(!serverSocket.isClosed()) {
                        Socket client = serverSocket.accept();
                        InstructionSender = new DataOutputStream(client.getOutputStream());
                        InstructionReader = new BufferedReader(new InputStreamReader(client.getInputStream()));

                        while(client.isConnected()){
                            try{
                                //Log.d("Socket","Connect success");
                                //Receive Instruction
                                String InstructionStr = InstructionReader.readLine();
                                JSONObject Instruction = new JSONObject(InstructionStr);
                                //Analyze Instruction Element
                                //String Instruction_type = Instruction.getString("type");
                                //Implement Instruction
                                runOnUiThread(() -> {
                                    ReceiveMessage.setText(InstructionStr);
                                });
                                Log.d("Zenbo_Say", "InstructionStr");
                            }catch (Exception e) {
                                Log.e("Socket Fail", "exception", e);
                            }
                        }
                    }
                }catch (Exception e) {
                    Log.e("Socket Fail",e.toString());
                }
            }
        });
        clientThread.start();
    }

    private void ServerSend(DataOutputStream InstructionSender){
        Thread clientThread = new Thread(new Runnable()
        {
            @Override
            public void run() {
                try {
                    JSONObject msg = new JSONObject();
                    msg.put("type", instruction_type.getText().toString());
                    msg.put("table_id", table_id.getText().toString());
                    msg.put("x", table_id.getText().toString());
                    msg.put("y", table_id.getText().toString());
                    InstructionSender.write((msg.toString() + "\n").getBytes());
                    InstructionSender.flush();
                }catch (Exception e) {
                    Log.e("Socket Fail",e.toString());
                }
            }
        });
        clientThread.start();
    }

    public static  String getLocalIpAddress(){
        try{
            for(Enumeration<NetworkInterface> en = NetworkInterface.getNetworkInterfaces(); en.hasMoreElements();){
                NetworkInterface intf = en.nextElement();
                for(Enumeration<InetAddress> enumIpAddr = intf.getInetAddresses(); enumIpAddr.hasMoreElements();){
                    InetAddress inetAddress = enumIpAddr.nextElement();
                    if (!inetAddress.isLoopbackAddress() && inetAddress instanceof Inet4Address) {
                        String ipaddress = inetAddress.getHostAddress().toString();
                        return ipaddress;
                    }
                }
            }
        } catch (SocketException e) {
            e.printStackTrace();
        }
        return  null;
    }
    /*
    private Runnable ReceiveInstruction = new Runnable() {
        @Override
        public void run() {
            Log.d("Socket", "ReceiveThread start Success");
            while(client.isConnected()){
                try{
                    //Receive Instruction
                    String InstructionStr = InstructionReader.readLine();
                    JSONObject Instruction = new JSONObject(InstructionStr);

                    //Analyze Instruction Element
                    String Instruction_type = Instruction.getString("Instruction_type");
                    int table_id = Instruction.getInt("table_id");
                    double x = Instruction.getDouble("x");
                    double y = Instruction.getDouble("y");

                    //Implement Instruction
                    ReceiveMessage.setText(InstructionStr);
                }catch (Exception e) {
                    Log.e("Socket Fail", "exception", e);
                }
            }
        }
    };
    */

}