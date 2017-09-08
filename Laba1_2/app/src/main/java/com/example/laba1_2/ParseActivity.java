package com.example.laba1_2;

import android.app.ProgressDialog;
import android.os.AsyncTask;
import android.support.v7.app.ActionBarActivity;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

import org.htmlcleaner.CleanerProperties;
import org.htmlcleaner.HtmlCleaner;
import org.htmlcleaner.TagNode;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.net.URL;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

public class ParseActivity extends ActionBarActivity {

    Button btnFillIn;
    TextView textView;
    private ListView lv;
    private ArrayAdapter<String> adapter;
    public ArrayList<String> titleList = new ArrayList<String>();

    private ProgressDialog pd;
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_parse);

        btnFillIn = (Button) findViewById(R.id.buttonParse);
        btnFillIn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                MyTask mt = new MyTask();
                mt.execute();
            }
        });
        lv = (ListView) findViewById(R.id.listView1);
        // Добавляем данные для ListView
        adapter = new ArrayAdapter<String>(this, R.layout.list_item, R.id.product_name, titleList);

        //textView = (TextView)findViewById(R.id.textView1);


    }


    class MyTask extends AsyncTask<Void, Void, Void> {

        String title;//Тут храним значение заголовка сайта

        @Override
        protected Void doInBackground(Void... params) {

            Document doc = null;//Здесь хранится будет разобранный html документ

            try {
                //Считываем заглавную страницу http://harrix.org
                doc = Jsoup.connect("http://blog.harrix.org/").get();
                Elements links = doc.select("a[href]");
                titleList.clear();
                for (Element link : links)
                {
                    titleList.add(link.text());
                }
            } catch (IOException e) {
                //Если не получилось считать

                e.printStackTrace();
            }

            //Если всё считалось, что вытаскиваем из считанного html документа заголовок
          /*  if (doc != null)
                title = doc.title();
            else
                title = "Ошибка";*/

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
