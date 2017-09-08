package com.example.laba1_2;

import android.app.ProgressDialog;
import android.content.Intent;
import android.os.AsyncTask;
import android.support.v7.app.ActionBarActivity;
import android.os.Bundle;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.Toast;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.util.ArrayList;

public class ParseActivity extends ActionBarActivity {

    Button btnFillIn, btnAddInBD;
    private ListView lv;
    private ArrayAdapter<String> adapter;
    public ArrayList<String> titleList = new ArrayList<String>();
    private Database1 database;

    private ProgressDialog pd;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_parse);

        database = new Database1(this);
        database.open();

        btnAddInBD = (Button) findViewById(R.id.buttonAddInBD);
        btnAddInBD.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (titleList != null){
                    database.delFromTheme();
                    for (String it : titleList){
                        database.addRecTheme(it);
                    }
                    Toast.makeText(ParseActivity.this, "добавлено " + titleList.size() + " записей", Toast.LENGTH_SHORT).show();
                    database.PrintAllTheme();
                }

            }
        });


        btnFillIn = (Button) findViewById(R.id.buttonParse);
        btnFillIn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                MyTask mt = new MyTask();
                mt.execute();
            }
        });
        lv = (ListView) findViewById(R.id.listView1);
        lv.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
                Intent intent = new Intent(view.getContext(), OpenPage.class);
                intent.putExtra("ssel", "https://www.buzzfeed.com/expresident/best-cat-pictures?utm_term=.cg5kBV8Qr#.rx0K9nXRv");
                startActivity(intent);
            }
        });
        // Добавляем данные для ListView
        adapter = new ArrayAdapter<String>(this, R.layout.list_item, R.id.product_name, titleList);

        //textView = (TextView)findViewById(R.id.textView1);

    }

    protected void onDestroy() {
        super.onDestroy();
        database.close();
    }
    class MyTask extends AsyncTask<Void, Void, Void> {

        String title;//Тут храним значение заголовка сайта

        @Override
        protected Void doInBackground(Void... params) {

            Document doc = null;//Здесь хранится будет разобранный html документ

            try {
                //Считываем заглавную страницу http://harrix.org
                doc = Jsoup.connect("https://www.buzzfeed.com/expresident/best-cat-pictures?utm_term=.cg5kBV8Qr#.rx0K9nXRv").get();
                Elements links = doc.select("span.js-subbuzz__title-text");//h2.entry-title
                titleList.clear();

                for (Element link : links) {
                    titleList.add(link.text());
                }

            } catch (IOException e) {
                //Если не получилось считать
                e.printStackTrace();
            }

            return null;
        }

        @Override
        protected void onPostExecute(Void result) {
            super.onPostExecute(result);
            lv.setAdapter(adapter);
            //textView.setText(title);
        }
    }


}
