   M S B A M L   `   `   `  ����E  AVSA Viewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:VSA_Viewer.ViewVSA_Viewer.View  5   	   ;"clr-namespace:VSA_Viewer.ViewModelVSA_Viewer.ViewModel  5   	   $    VSA_Viewer.View.SettingsWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:VSA_Viewer.View     5   	   1vm"clr-namespace:VSA_Viewer.ViewModel     5   	   -   5          � WindowStartupLocation$  CenterScreen=�5	   	    � 
ResizeMode$ NoResize=�5   	    � WindowStyle$
 None=�5   	      VSABlue#��[  5   	   $��2q�5   	    � Title$ SettingsWindow��5   	   $	��450��6    $	��800��6-   ��5      �� 5   
    �� ImageSource$  vsa-background-450x800.png��6    � DataContext 5      %   VSA_Viewer.ViewModel.ImageSetVM  5   
   .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $��* �6   �� 5      $��* �6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5!   
   �� $��Settings��6   $��1��5"      #��[  5#      $��30(�5$      $��Center=�5%      $��	0,20,0,10q�5&      �� 5'   
   $��1��6   $��0��5(      $��3��5)      $��	10,30,0,0q�5*      .+�5+      �� $��Current Save Path: ��6   
�������$��15(�5-      �� 5.      
�������$��15(�50      ��5.      �� *
SavePath+50      51      �� 52   
   $��Set New Save Path��6   $��1��53      $��1��54        MenuButtons#��[ 55      $��Bottom=�57      ��56      �� *SetNewSavePathCommand+57      �� 59   
   -   5         ��Name$ 
doneButton��59      $
��Done��5:      #��[ 5;      $��2��5<      $��1��5=      $��10,10,10,30q�5?      5B      5C      