   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.2.4.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   
    TimeSpender   TimeSpender.MainWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   
   J FWpfAnimatedGif, Version=1.4.14.0, Culture=neutral, PublicKeyToken=null,gif"http://wpfanimatedgif.codeplex.com  5      -   5          � Title$  TIMESPENDER��5   	   $	��350��6   $	��525��6*   $	��525��66   $	��525��6E   $	��350��6T   $	��350��6d      AccentColorBrush#���   5      $��1q�5       � WindowStartupLocation$ CenterScreen=�5
      .��5      �  � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   ��  �� TabStripPlacement$
 Left=�6   $
��Left=�6.   $	��259��6I   $��	10,49,0,0q�6V   $	��Top=�6i   $	��495��6�   
�������$��2��6�   
�������.@�5      �� $
��Main��6   .��5      � 
����� �.+�5      �� -   5         ��Name$ main_combobox��5      $
��Left=�66   $��	166,9,0,0q�6Q   $	��Top=�6d   $	��120��6|   �� 5      $��1. Choose existing��6   $
��Left=�69   $��	10,10,0,0q�6T   $	��Top=�6g   $��Arial)�6   $
��Bold"�6�   $��16(�6�   
�������6�   �� 5      -   5         ��Name$ 
label_time��5      $��Label��60   $
��Left=�6@   $��
250,35,0,0q�6[   $	��Top=�6o   $	��110��6�   �� 5      $��or��6   $
��Left=�6)   $��	66,38,0,0q�6D   $	��Top=�6W   $��Arial)�6o   $
��Bold"�6�   $��16(�6�   
�������6�   �� 5      $��
Create new��6   $
��Left=�61   $��	32,66,0,0q�6L   $	��Top=�6_   $��Arial)�6w   $
��Bold"�6�   $��16(�6�   
�������6�   �� 5      -   5         ��Name$ main_textbox��5      $
��Left=�64   $��23��6O   $��
166,66,0,0q�6[    �� TextWrapping$
 Wrap=�6o   $�� ��6�   $	��Top=�6�   $	��120��6�   �� 5      -   5         ��Name$ main_image_galka��5      $��ico_check.png��67   $
��Left=�6N   $��30��6i   $��
291,64,0,0q�6u   $	��Top=�6�   $��32��6�   6�   ]� 5      $
��Left=�6    $	��178��6;   ��.  $��	0,110,0,0q�6^   $	��Top=�6q   $	��388��6�   ���	#FFEEEEEE6�   �� 5      -   5        $ 
main_start��5      $��START��60   $
��Left=�6@   $��130,112,0,0q�6[   $	��Top=�6p   $
��Bold"�6�   $��Italic$�6�   $��36(�6�   
�������	 J� Effect	 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5!      -   5        $ 	main_stop��5!      $
��STOP��6/   $
��Left=�6>   $��130,175,0,0q�6Y   $	��Top=�6n   $
��Bold"�6�   $��Italic$�6�   $��36(�6�   
�������	 5"        5#      5%      �� 5&      -   5        $ main_startGif��5&      "  WpfAnimatedGif.ImageBehavior
   RepeatBehavior$
 Forever��63      AnimatedSource$ 32.gif��5'      $��250,131,91,130q�66   $y�Hidden=�6N   5(      5)      �� 5*      $��Results��6   .��5+      � ���	#FFE5E5E5.+�5,      �� $��Total Hours��6   $
��Left=�62   $��	10,46,0,0q�6M   $	��Top=�6`   $��16(�6x   $��Italic$�6�   �� 5-      -	   5        $ results_combobox��5-      $
��Left=�69   $��
105,10,0,0q�6T   $	��Top=�6h   $	��176��6�   $��31��6�   �� 5.      -
   5        $ label_TotalHours��5.      $�� ��66   $
��Left=�6A   $��
105,46,0,0q�6\   $	��Top=�6p   $��15(�6�   $
��Bold"�6�   ���	#FFFF46006�   �� 5/      -   5         ��Name$ results_button��5/      $	��Go!��65   $
��Left=�6C   $��
286,10,0,0q�6^   $	��Top=�6r   $��75��6�   $��31��6�   6�   50      51      52      o� 53   
   $��2��6   $
��Left=�6#   $��34��6>   $��	10,10,0,0q�6J   $	��Top=�6]   $	��495��6u   .@�54      m� -   5        $��Settings��54      $��34��6)   .@�55      m� -   5         m�Name$ menu_connectiontomysql��55      $��Connection to MySql��6;   56      58      �� 59   
   -   5        $ label_gohere��59      $��Go here and Set It��6&   $��1��6C   $
��Left=�6S   $��	69,10,0,0q�6n   $	��Top=�6�   
���  ��$
��Bold"�6�   $��15(�6�   $y�Hidden=�6�   5<      5=      