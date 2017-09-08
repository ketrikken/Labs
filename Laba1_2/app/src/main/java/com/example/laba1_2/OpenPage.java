package com.example.laba1_2;

import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.webkit.WebView;
import android.widget.Toast;

public class OpenPage extends AppCompatActivity {

    String path;
    WebView web;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_open_page);
        web = (WebView) findViewById(R.id.webPage);
        try{
            path = getIntent().getStringExtra("ssel");
            web.loadUrl(path);
        } catch(Throwable t) {
            Toast.makeText(getApplicationContext(), "Exception: " + t.toString(), Toast.LENGTH_LONG).show();
        }
    }
}
