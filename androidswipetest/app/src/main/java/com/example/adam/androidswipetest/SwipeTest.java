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
    private static long startTime;

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
    private Point pointOrigin;
    private Point lastPoint;
    private LinkedList<Point> pointHistory = new LinkedList<>();

    //gradient variables
    private float gradient;
    private LinkedList<Float> gradientHistory = new LinkedList<>();
    private int grandientHistorySize = 5;
    private float movingAverageGradient;

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
        Log.d("EVENT", "-------- " + counter + " --------");
        Log.d("Attributes", writeEventAttributes(event));

        //Set lastpoint - null pointer exception without check
        if (!pointHistory.isEmpty()) {
            lastPoint = pointHistory.getLast();
        }

        //Set current point
        int x = (int) event.getX();
        int y = (int) event.getY();
        point = new Point(x, y);

        //getAction() produces the type of event, currently only handling DOWN, MOVE, UP with 0, 1, 2 respectively
        int action = event.getAction();

        //reset gradient to sum again
        float sumGradient = 0;

        /** action start
         * sets origin
         * reset pointHistory as it is a new swipe
         * adds to history as first entity
         */
        if (action == MotionEvent.ACTION_DOWN) {
            Log.d("State", "Begin");
            pointOrigin = point;
            pointHistory = new LinkedList<Point>();
            pointHistory.add(point);
        }

        /** action move
         * adds to history
         */
        if (action == MotionEvent.ACTION_MOVE) {
            Log.d("State", "Move");
            pointHistory.add(point);
        }

        /** action up
         * resets counter
         * adds to history
         */
        if (action == MotionEvent.ACTION_UP) {
            Log.d("State", "End");
            pointHistory.add(point);
            Point deltaPoint = calculateDelta(point, pointOrigin);
            gradient = calculatedGradient(deltaPoint);
            Log.d("Input", String.valueOf(gradient));
            counter = 0;
            double angle = Math.toDegrees(Math.atan(gradient));
            Log.d("Angle", String.valueOf(angle));
            
            return true;
        }
        Log.d("History", writePointHistory(pointHistory));

        //calculating gradient
        //cannot calculate if it is the first action
        if (action == MotionEvent.ACTION_MOVE || action == MotionEvent.ACTION_UP) {
            //call calculatedGradient method
            gradient = calculatedGradient(calculateDelta(point, lastPoint));
            if (!gradientHistory.isEmpty()) {
                //calculates movingAverageGradient
                for (int i = 0; i < gradientHistory.size(); i++) {
                    sumGradient = sumGradient + gradientHistory.get(i);
                    movingAverageGradient = sumGradient / gradientHistory.size();
                }
                //if current size is equal to the size we set then remove last and add new
                if (gradientHistory.size() >= grandientHistorySize) {
                    gradientHistory.removeLast();
                }
            }
            //else just add normally
            gradientHistory.add(gradient);
        }
        Log.d("Average Gradient", String.valueOf(movingAverageGradient));
        Log.d("Gradient", String.valueOf(gradient));
        return true;
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
