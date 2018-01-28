package com.example.adam.androidswipetest;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.MotionEvent;

import java.util.LinkedList;
import java.util.List;

import static java.lang.Math.abs;

public class SwipeTest extends AppCompatActivity {

    private static int counter = 0;
    private static long startTime;

    public class Point {
        private int x;
        private int y;

        public Point(int x, int y) {
            this.x = x;
            this.y = y;
        }

        public int getX() {
            return x;
        }

        public void setX(int x) {
            this.x = x;
        }

        public int getY() {
            return y;
        }

        public void setY(int y) {
            this.y = y;
        }

        public String readPoint() {
            return this.getX() + ", " + this.getY();
        }
    }

    private Point point;
    private Point pointOrigin;
    private Point lastPoint;
    private LinkedList<Point> pointHistory = new LinkedList<>();

    private float gradient;
    private LinkedList<Float> gradientHistory = new LinkedList<>();
    private int grandientHistorySize = 5;
    private float movingAverageGradient;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_swipe_test);
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {
        Point testCurrent = new Point(900, 2);
        Point testLast = new Point(500, 1);
        Log.d("TestGradient", String.valueOf(calculatedGradient(testCurrent, testLast)));
        counter++;
        Log.d("EVENT", "-------- " + counter + " --------");
        Log.d("Attributes", writeAttributes(event));
        if (!pointHistory.isEmpty()) {
            lastPoint = pointHistory.getLast();
        }

        int x = (int) event.getX();
        int y = (int) event.getY();
        point = new Point(x, y);
        int action = event.getAction();
        float sumGradient = 0;


        if (action == MotionEvent.ACTION_DOWN) {
            Log.d("State", "Begin");
            pointOrigin = point;
            pointHistory = new LinkedList<Point>();
            pointHistory.add(point);
        }
        if (action == MotionEvent.ACTION_MOVE) {
            Log.d("State", "Move");
            pointHistory.add(point);
        }
        if (action == MotionEvent.ACTION_UP) {
            Log.d("State", "End");
            pointHistory.add(point);
            counter = 0;
        }
        Log.d("History", showHistory(pointHistory));
        if (action == MotionEvent.ACTION_MOVE || action == MotionEvent.ACTION_UP) {
            gradient = calculatedGradient(point, lastPoint);
            if (!gradientHistory.isEmpty()) {
                for (int i = 0; i < gradientHistory.size(); i++) {
                    sumGradient = sumGradient + gradientHistory.get(i);
                    movingAverageGradient = sumGradient / gradientHistory.size();
                }
                if (gradientHistory.size() > grandientHistorySize) {
                    gradientHistory.removeLast();
                }
            }
            gradientHistory.add(gradient);
        }
        Log.d("Average Gradient", String.valueOf(movingAverageGradient));
        Log.d("Gradient", String.valueOf(gradient));
        return true;
    }

    public float calculatedGradient(Point current, Point last) {
        float deltaX = current.x - last.x;
        float deltaY = current.y - last.y;
        if(deltaX != 0){
            float gradient = deltaY/ deltaX;
            return gradient;
        }
        else {
            return 0;
        }
    }

    public Point createRelativePoint(Point origin, Point current) {
        return new Point(current.x - origin.x, current.y - origin.x);
    }

    public String showHistory(List<Point> points) {
        StringBuilder sb = new StringBuilder();
        for(int i = 0; i < points.size(); i++) {
            if(i == 0) {
                sb.append(i + ": ");
            }
            else {
                sb.append(", " + i + ": ");
            }
            sb.append(points.get(i).readPoint());
        }
        return sb.toString();
    }

    public String writeAttributes(MotionEvent event) {
        StringBuilder sb = new StringBuilder();
        if(counter == 1) {
            startTime = event.getEventTime();
        }
        long currentTime = event.getEventTime() - startTime;
        sb.append("Time: ");
        sb.append(currentTime);
        sb.append(", Point: ");
        sb.append(new Point(((int) event.getX()), (int) event.getY()).readPoint());
        return sb.toString();
    }
}
