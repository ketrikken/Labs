package com.example.laba1_2;

import android.content.Context;
import android.database.Cursor;
import android.database.sqlite.SQLiteDatabase;
import android.util.Log;

import java.util.ArrayList;
import java.util.List;

/**
 * Created by Алатиэль on 08.09.2017.
 */

public class Database1 {
    private DBHelper dbHelper;
    private SQLiteDatabase database;
    private final Context myContext;

    public Database1(Context ctx) {
        myContext = ctx;
    }

    // открыть подключение
    public void open() {

        dbHelper = new DBHelper(myContext, DBHelper.DATABASE_NAME, null, DBHelper.DATABASE_VERSION);
        database = dbHelper.getWritableDatabase();
    }

    // закрыть подключение
    public void close() {
        if (dbHelper!=null) dbHelper.close();
    }

    public List<String> selectAll() {
        List<String> list = new ArrayList<>();
        Cursor cursor = database.query(DBHelper.TABLE_THEMES, new String[] { DBHelper.THEM_HEADER },
                null, null, null, null, null);

        if (cursor.moveToFirst()) {
            do {
                list.add(cursor.getString(0));

            } while (cursor.moveToNext());
        }
        if (cursor != null && !cursor.isClosed()) {
            cursor.close();
        }
        return list;
    }
    void delFromTheme() {
        database.delete(DBHelper.TABLE_THEMES, DBHelper.EXTERNAL_KEY_ID + " >= 0" , null);
    }
    // получить все данные из таблицы DB_TABLE
    Cursor getAllDataTheme() {
        return database.query(DBHelper.TABLE_THEMES, null, null, null, null, null, null);
    }
    void addRecTheme(String text){
        String textNew = new String();
        for (int i = 0; i < text.length(); ++i) {
            textNew += text.charAt(i);
            if (text.charAt(i) == '\''){
                textNew += text.charAt(i);
            }
        }

        database.execSQL("INSERT INTO "+ DBHelper.TABLE_THEMES + " VALUES ( null, " + "'" + textNew + "' );");
        //PrintAllTheme();
    }
    public void PrintAllTheme()
    {
        Cursor cursorr = getAllDataTheme();
        // startManagingCursor(cursorr);
        Log.d("mLog", "------------------print-----------------------");
        if (cursorr.moveToFirst()) {
            int idIndex = cursorr.getColumnIndex(DBHelper.EXTERNAL_KEY_ID);
            int nameIndex = cursorr.getColumnIndex(DBHelper.THEM_HEADER);
            do {
                Log.d("mLog", "ID = " + cursorr.getInt(idIndex) +
                        ", theme = " + cursorr.getString(nameIndex));
            } while (cursorr.moveToNext());
        } else
            Log.d("mLog","0 rows");

        cursorr.close();
    }
}
