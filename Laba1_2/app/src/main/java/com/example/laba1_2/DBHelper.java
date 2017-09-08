package com.example.laba1_2;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;
import android.util.Log;

/**
 * Created by Алатиэль on 07.09.2017.
 */

public class DBHelper extends SQLiteOpenHelper {

    public static final int DATABASE_VERSION = 4;
    public static final String DATABASE_NAME = "themesDB";
    public static final String TABLE_THEMES = "theme";
    public static final String EXTERNAL_KEY_ID = "_id";

    public static final String THEM_HEADER = "header";



    /* public DBHelper(Context context) {
         super(context, DATABASE_NAME, null, DATABASE_VERSION);
     }*/
    public DBHelper(Context context, String name, SQLiteDatabase.CursorFactory factory, int version) {
        super(context, name, factory, version);
    }
    @Override
    public void onCreate(SQLiteDatabase db) {
        Log.d("mLog", "-------------------onCreate---------------");
        CreateTableTheme(db);
    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
        if (newVersion >  oldVersion){

            Log.d("mLog", oldVersion + " " + newVersion);
            Log.d("mLog", "-------------------onUpdate---------------");
            db.execSQL("drop table if exists " + TABLE_THEMES);
            onCreate(db);

        }

    }

    private void CreateTableTheme(SQLiteDatabase db){
        db.execSQL("create table " + TABLE_THEMES + "(" +
                EXTERNAL_KEY_ID + " INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL," +
                THEM_HEADER + " TEXT );");
        Log.d("mLog", " --- CREATE theme NOTE ---- ");
    }


}



