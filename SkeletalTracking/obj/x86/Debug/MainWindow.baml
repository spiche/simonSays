   M S B A M L   `   `   `  ����^  ZMicrosoft.Samples.Kinect.WpfViewers, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null�^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers#Microsoft.Samples.Kinect.WpfViewers  5   	    SkeletalTracking!   SkeletalTracking.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   mmy^clr-namespace:Microsoft.Samples.Kinect.WpfViewers;assembly=Microsoft.Samples.Kinect.WpfViewers     5   	   -   5          � Title$  
MainWindow��5   	   $	��600��6   $	��800��6)    � WindowState$ 	Maximized=�5   "   .��5      �� -   5        $��
MainCanvas��5      .+�5   
   ;   5Microsoft.Samples.Kinect.WpfViewers.KinectColorViewer  -   5        $��kinectColorViewer1��5   W    �� Left$ 0��6   
 �� Top$ 0��6/   $	��603��6>   $	��372��6J   5   /Microsoft.Samples.Kinect.WpfViewers.ImageViewer   Kinect 5	      ��  �� ElementName$ kinectSensorChooser1�� �� Path$ Kinect�A� 5
   
   -   5        $��leftEllipse��5
   =   $ 0��6   $ 0��6"   $��50��61   $��50��6Q   ���	#FF4D298D J� Opacity$ 1Q�6m   
�������6y   A� 5   
   -   5        $��rightEllipse��5   P   $	 100��6   $ 0��6$   ���	#FF2CACE3$��50��6D   $��50��6d   $ 1Q�6o   
�������6{   =   7Microsoft.Samples.Kinect.WpfViewers.KinectSensorChooser  5   
   -   5        $��kinectSensorChooser1��5   D   $	 250��6!   $	 380��63   $	��328��6`   �� 5   
   -   5        $��label1��5   K   $ 0��6   $ 0��6    $��Label��6/   $��95��6?   $	��168��6Y   
���   ���6x   Z� A� 5   
   -   5        $��	redCircle��5   @   $	 250��6   $ 15��6$   $��80��64   $��82��6c   
���  ����6Q   Z� 6n   A� 5   
   -	   5        $��
blueCircle��5   @   $ 86��6   $	 194��6#   $��80��64   $��82��6d   
����  ���6R   Z� 6o   A� 5   
   -
   5        $��greenCircle��5   A   $	 428��6   $	 194��6$   $��80��65   $��82��6f   
��� � ���6T   Z� 6q   5      5      