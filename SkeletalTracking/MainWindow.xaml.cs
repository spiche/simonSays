// (c) Copyright Microsoft Corporation.
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Kinect;
using Coding4Fun.Kinect.Wpf;

using System.Timers;
using System.Windows.Threading; 

namespace SkeletalTracking
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }


        SolidColorBrush[] colors = { Brushes.Red, Brushes.Green, Brushes.Blue };
        DispatcherTimer timer1;
        int t;

        int color;
        Random random;
        int rand;
        bool enabled = false;

        bool closing = false;
        const int skeletonCount = 6; 
        Skeleton[] allSkeletons = new Skeleton[skeletonCount];
        


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            kinectSensorChooser1.KinectSensorChanged += new DependencyPropertyChangedEventHandler(kinectSensorChooser1_KinectSensorChanged);

            t = 3;

            timer1 = new DispatcherTimer();
            timer1.Tick += new EventHandler(timer_Tick);
            timer1.Interval = new TimeSpan(0, 0, 1);  //this is (hours,minutes,seconds) format
            timer1.Start();
            color = 0;

                        
            random = new Random();
            

        }

        void timer_Tick(object sender, EventArgs e)
        {
            label1.Background = Brushes.Black;
            if (t > 0)
            {
                t--;
            }
            else
            {
                timer1.Stop();
                label1.Background = colors.ElementAt(0);
            }
        }

        
        void startSequence()
        {
            // colors = { Red, Green, Blue }
         
            if (color == 0)
            {
                label1.Background = colors.ElementAt(1);
                enabled = false;
            }
            if (color == 1)
            {
                label1.Background = colors.ElementAt(2);
                enabled = false;
            }
            if (color == 2)
            {
                label1.Background = colors.ElementAt(0);
                enabled = false;
            }



        }

        void kinectSensorChooser1_KinectSensorChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            KinectSensor old = (KinectSensor)e.OldValue;

            StopKinect(old);

            KinectSensor sensor = (KinectSensor)e.NewValue;

            if (sensor == null)
            {
                return;
            }

            


            var parameters = new TransformSmoothParameters
            {
                Smoothing = 0.3f,
                Correction = 0.0f,
                Prediction = 0.0f,
                JitterRadius = 1.0f,
                MaxDeviationRadius = 0.5f
            };
            sensor.SkeletonStream.Enable(parameters);

            sensor.SkeletonStream.Enable();

            sensor.AllFramesReady += new EventHandler<AllFramesReadyEventArgs>(sensor_AllFramesReady);
            sensor.DepthStream.Enable(DepthImageFormat.Resolution640x480Fps30); 
            sensor.ColorStream.Enable(ColorImageFormat.RgbResolution640x480Fps30);

            try
            {
                sensor.Start();
            }
            catch (System.IO.IOException)
            {
                kinectSensorChooser1.AppConflictOccurred();
            }
        }

        void collision()
        {
            //are the hands colliding with the colored circles?

            double upperBoundRight = 0;
            double actualDistanceRight = 0;
            double upperBoundLeft = 0;
            double actualDistanceLeft = 0;

            //finds the center of the circle
            double redCenterX = Canvas.GetLeft(redCircle) + redCircle.Width / 2;
            double redCenterY = Canvas.GetTop(redCircle) + redCircle.Height / 2;

            double greenCenterX = Canvas.GetLeft(greenCircle) + greenCircle.Width / 2;
            double greenCenterY = Canvas.GetTop(greenCircle) + greenCircle.Height / 2;

            double blueCenterX = Canvas.GetLeft(blueCircle) + blueCircle.Width / 2;
            double blueCenterY = Canvas.GetTop(blueCircle) + blueCircle.Height / 2;

            //finding the center of the left hand ellipse
            double leftCenterX = Canvas.GetLeft(leftEllipse) + leftEllipse.Width / 2;
            double leftCenterY = Canvas.GetTop(leftEllipse) + leftEllipse.Height / 2;

            //finding the center of the right hand ellipse
            double rightCenterX = Canvas.GetLeft(rightEllipse) - rightEllipse.Width / 2;
            double rightCenterY = Canvas.GetTop(rightEllipse) - rightEllipse.Height / 2;

            //collision with RED
            if (color == 0)
            {
                upperBoundLeft = redCircle.Width / 2 + leftEllipse.Width / 2;
                actualDistanceLeft = Math.Sqrt(Math.Pow(redCenterX - leftCenterX, 2)
                    + Math.Pow(redCenterY - leftCenterY, 2));
                upperBoundRight = redCircle.Width / 2 + rightEllipse.Width / 2;
                actualDistanceRight = Math.Sqrt(Math.Pow(redCenterX - rightCenterX, 2)
                    + Math.Pow(redCenterY - rightCenterY, 2));
            }
            //collision with GREEN
            if (color == 1)
            {
                upperBoundLeft = greenCircle.Width / 2 + leftEllipse.Width / 2;
                actualDistanceLeft = Math.Sqrt(Math.Pow(greenCenterX - leftCenterX, 2)
                    + Math.Pow(greenCenterY - leftCenterY, 2));
                upperBoundRight = greenCircle.Width / 2 + rightEllipse.Width / 2;
                actualDistanceRight = Math.Sqrt(Math.Pow(greenCenterX - rightCenterX, 2)
                    + Math.Pow(greenCenterY - rightCenterY, 2));
                
            }
            //collision with BLUE
            if (color == 2)
            {
                upperBoundLeft = blueCircle.Width / 2 + leftEllipse.Width / 2;
                actualDistanceLeft = Math.Sqrt(Math.Pow(blueCenterX - leftCenterX, 2)
                    + Math.Pow(blueCenterY - leftCenterY, 2));
                upperBoundRight = blueCircle.Width / 2 + rightEllipse.Width / 2;
                actualDistanceRight = Math.Sqrt(Math.Pow(blueCenterX - rightCenterX, 2)
                    + Math.Pow(blueCenterY - rightCenterY, 2));
                
            }

            if (actualDistanceLeft < upperBoundLeft || actualDistanceRight < upperBoundRight)
            {
                enabled = true;
            }


            if (color < 2)
            {
                color++;
            }
            else
            {
                color = 0;
            }

            if (enabled)
            {
                startSequence();
            }

        }

        void sensor_AllFramesReady(object sender, AllFramesReadyEventArgs e)
        {
            
            if (closing)
            {
                return;
            }

            //Get a skeleton
            Skeleton first =  GetFirstSkeleton(e);

            if (first == null)
            {
                return; 
            }            
            
            //set scaled position
            //ScalePosition(headImage, first.Joints[JointType.Head]);
            ScalePosition(leftEllipse, first.Joints[JointType.HandLeft]);
            ScalePosition(rightEllipse, first.Joints[JointType.HandRight]);

            GetCameraPoint(first, e);
            collision();

        }

        void GetCameraPoint(Skeleton first, AllFramesReadyEventArgs e)
        {

            using (DepthImageFrame depth = e.OpenDepthImageFrame())
            {
                if (depth == null ||
                    kinectSensorChooser1.Kinect == null)
                {
                    return;
                }


                //Map a joint location to a point on the depth map
                //head
                //DepthImagePoint headDepthPoint =
                //    depth.MapFromSkeletonPoint(first.Joints[JointType.Head].Position);
                //left hand
                DepthImagePoint leftDepthPoint =
                    depth.MapFromSkeletonPoint(first.Joints[JointType.HandLeft].Position);
                //right hand
                DepthImagePoint rightDepthPoint =
                    depth.MapFromSkeletonPoint(first.Joints[JointType.HandRight].Position);


                //Map a depth point to a point on the color image
                //head
                //ColorImagePoint headColorPoint =
                //    depth.MapToColorImagePoint(headDepthPoint.X, headDepthPoint.Y,
                //    ColorImageFormat.RgbResolution640x480Fps30);
                //left hand
                ColorImagePoint leftColorPoint =
                    depth.MapToColorImagePoint(leftDepthPoint.X, leftDepthPoint.Y,
                    ColorImageFormat.RgbResolution640x480Fps30);
                //right hand
                ColorImagePoint rightColorPoint =
                    depth.MapToColorImagePoint(rightDepthPoint.X, rightDepthPoint.Y,
                    ColorImageFormat.RgbResolution640x480Fps30);


                //Set location
                //CameraPosition(headImage, headColorPoint);
                CameraPosition(leftEllipse, leftColorPoint);
                CameraPosition(rightEllipse, rightColorPoint);
            }        
        }


        Skeleton GetFirstSkeleton(AllFramesReadyEventArgs e)
        {
            using (SkeletonFrame skeletonFrameData = e.OpenSkeletonFrame())
            {
                if (skeletonFrameData == null)
                {
                    return null; 
                }

                
                skeletonFrameData.CopySkeletonDataTo(allSkeletons);

                //get the first tracked skeleton
                Skeleton first = (from s in allSkeletons
                                         where s.TrackingState == SkeletonTrackingState.Tracked
                                         select s).FirstOrDefault();

                return first;

            }
        }

        private void StopKinect(KinectSensor sensor)
        {
            if (sensor != null)
            {
                if (sensor.IsRunning)
                {
                    //stop sensor 
                    sensor.Stop();

                    //stop audio if not null
                    if (sensor.AudioSource != null)
                    {
                        sensor.AudioSource.Stop();
                    }


                }
            }
        }

        private void CameraPosition(FrameworkElement element, ColorImagePoint point)
        {
            //Divide by 2 for width and height so point is right in the middle 
            // instead of in top/left corner
            Canvas.SetLeft(element, point.X - element.Width / 2);
            Canvas.SetTop(element, point.Y - element.Height / 2);

        }

        private void ScalePosition(FrameworkElement element, Joint joint)
        {
            //convert the value to X/Y
            Joint scaledJoint = joint.ScaleTo(1280, 720); 
            
            //convert & scale (.3 = means 1/3 of joint distance)
           // Joint scaledJoint = joint.ScaleTo(1280, 720, .3f, .3f);

            Canvas.SetLeft(element, scaledJoint.Position.X);
            Canvas.SetTop(element, scaledJoint.Position.Y); 
            
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            closing = true; 
            StopKinect(kinectSensorChooser1.Kinect); 
        }



    }
}
