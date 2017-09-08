package com.example.laba1_2;

import android.content.Intent;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.Button;

public class StartActivity extends AppCompatActivity {

    int REQUEST_CODE = 1;
    Button btnCats, btnNeCats;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_start);

        btnNeCats = (Button) findViewById(R.id.buttonNeCats);
        btnNeCats.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Intent intent = new Intent(v.getContext(), ParseActivity.class);
                startActivityForResult(intent, REQUEST_CODE);
            }
        });
    }
}
