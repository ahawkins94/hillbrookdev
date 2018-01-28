package com.example.adam.androidswipetest;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.MotionEvent;

import java.util.Map;

public class SwipeTest extends AppCompatActivity {

    private Map<Integer, Integer> point;

    private double gradient;
    private double[] gradientHistory;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_swipe_test);
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        int x = (int)event.getX();
        int y = (int)event.getY();
        switch (event.getAction()) {
            case MotionEvent.ACTION_DOWN: Log.d("BEGIN", "BEGIN");


            case MotionEvent.ACTION_MOVE: Log.d("MOVE", "MOVE");


            case MotionEvent.ACTION_UP: Log.d("UP", "UP");
        }
        return true;
    }

}
