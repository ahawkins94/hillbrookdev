package com.example.adam.androidswipetest;

import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.MotionEvent;

import java.util.LinkedList;
import java.util.List;

import static java.lang.Math.abs;

public class SwipeTest extends AppCompatActivity {

    //class constants
    private static int counter = 0;
    private static int incrementalCounter = 0;
    private static long incrementalTime = 100;

    private static boolean changeDirection = false;

    private static long startTime;
    private static long currentTime;

    private final static double pi = 3.14;

    /**
     * Class to store point data, lower memory than MotionEvent class.
     * Potentially add Gradiant and Time to each point
     */
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

    //point variables
    private Point point;
    private Point pointOrigin = null;
    private Point lastPoint;
    private LinkedList<Point> pointHistory = new LinkedList<>();
    double angle;
    double deltaAngle;

    Point changePoint;
    Point deltaPoint;

    //gradient variables
    private float gradient;
    private LinkedList<Float> gradientHistory = new LinkedList<>();
    private int grandientHistorySize = 5;
    private float movingAverageGradient;

    private double lastAngle;

    //method to start android emulator
    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_swipe_test);
    }

    @Override
    public boolean onTouchEvent(MotionEvent event) {

        //Ease of reading events
        counter++;
        if(counter == 1) {
            startTime = event.getEventTime();
        }
        currentTime = event.getEventTime() - startTime;
        swipeControls(event);
        return true;
    }

    /**
     *
     */
    public void swipeControls(MotionEvent event) {

        int x = (int) event.getX();
        int y = (int) event.getY();
        point = new Point(x,y);
        int action = event.getAction();
        if(counter > 1){
            if(currentTime > incrementalCounter * incrementalTime) {
                deltaPoint = calculateDelta(point, lastPoint);
                angle = Math.atan2(deltaPoint.getX(), deltaPoint.getY()) + pi;

                lastPoint = point;
                if(incrementalCounter > 1) {
                    deltaAngle = abs(angle - lastAngle);
                    if(deltaAngle > pi/4 && !changeDirection) {
                        deltaPoint = calculateDelta(point, pointOrigin);
                        angle = Math.atan2(deltaPoint.getX(), deltaPoint.getY()) + pi;
                        changeDirection = true;
                        changePoint = point;
                        String input = calculateInput(angle);
                        Log.d("EVENT CHANGE", "-------- " + counter + " --------");
                        Log.d("Input", input);

                    }
                }
                lastAngle = angle;
                incrementalCounter++;

            }
        }

        if (action == MotionEvent.ACTION_DOWN) {
            pointOrigin = point;
            lastPoint = point;
            incrementalCounter = counter;
        }

        /** action move
         * adds to history
         */
        if (action == MotionEvent.ACTION_MOVE) {

        }

        if (action == MotionEvent.ACTION_UP) {
            Log.d("EVENT FINAL", "-------- " + counter + " --------");
            if(changeDirection) {
                deltaPoint = calculateDelta(point, changePoint);
            }
            else {
                deltaPoint = calculateDelta(point, pointOrigin);
            }
            changeDirection = false;
            angle = Math.atan2(deltaPoint.x, deltaPoint.y) + pi;
            counter = 0;
            String input = calculateInput(angle);
            Log.d("Input", input);
        }
    }

    public String calculateInputComplex(double angle) {
        if(angle < pi/6 || angle > 11*pi/6) {
            return "Up";
        }
        if(angle > pi/6 && angle < pi/3) {
            return "Up-Left";
        }
        if(angle > pi/3 && angle < 2*pi/3) {
            return "Left";
        }
        if(angle > 2*pi/3 && angle < 5*pi/6) {
            return "Down-Left";
        }
        if(angle > 5*pi/6 && angle < 7*pi/6) {
            return "Down";
        }
        if(angle > 7*pi/6 && angle < 4*pi/3) {
            return "Down-Right";
        }
        if(angle > 4*pi/3 && angle < 5*pi/3) {
            return "Right";
        }
        else {
            return "Up-Right";
        }
    }

    public String calculateInput(double angle) {
        if(angle < pi/4 || angle > 7*pi/4) {
            return "Up";
        }
        if(angle > pi/4 && angle < 3*pi/4) {
            return "Left";
        }
        if(angle > 3*pi/4 && angle < 5*pi/4) {
            return "Down";
        }
        else {
            return "Right";
        }
    }


    public float calculatedGradient(Point deltaPoint) {
        if(deltaPoint.getX() != 0){
            float gradient = deltaPoint.getY()/deltaPoint.getX();
            return gradient;
        }
        else {
            return 0;
        }
    }

    public Point calculateDelta(Point current, Point last) {
        return new Point(current.getX() - last.getX(), current.getY() - last.getY());
    }

    public Point createRelativePoint(Point origin, Point current) {
        return new Point(current.x - origin.x, current.y - origin.x);
    }

    public String writePointHistory(List<Point> points) {
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

    public String writeEventAttributes(MotionEvent event) {
        StringBuilder sb = new StringBuilder();
        sb.append("Time: ");
        sb.append(currentTime);
        sb.append(", Point: ");
        sb.append(new Point(((int) event.getX()), (int) event.getY()).readPoint());
        return sb.toString();
    }
}
