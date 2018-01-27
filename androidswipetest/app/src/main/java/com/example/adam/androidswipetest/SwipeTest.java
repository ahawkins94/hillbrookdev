package com.example.adam.androidswipetest;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;

public class SwipeTest extends AppCompatActivity {

    private Map<float, float> current;
    private (Map<float, float>)[]


    private double gradient;
    private double[] gradient history;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_swipe_test);
    }

    @Override
    public boolean onTouchEvent(MotionEvent even) {
        int x = (int)event.getX();
        int y = (int)event.getY();
        switch (event.getAction()) {
            case MotionEvent.ACTION_DOWN: Log.d("BEGIN", "BEGIN");


            case MotionEvent.ACTION_MOVE: Log.d("MOVE", "MOVE");


            case MotionEvent.ACTION_UP: Log.d("UP", "UP");
        }
    }

}
