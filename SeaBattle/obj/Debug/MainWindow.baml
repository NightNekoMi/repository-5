   M S B A M L   `   `   `  ����T  PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4fV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	    	SeaBattle   SeaBattle.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   PControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro     5   	   0ignorehttp://www.galasoft.ch/ignore     5   	   c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  5	   	     � Title$  
Sea Battle��5   	   $	��390��6   $	��812��6)    � DataContext 5   	   �� *Main+   Locator �� Source# [   � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $ Skins/MainSkin.xaml?�6%   %    5      .��5      �� -   5         ��Name$
 grid��5      
�������.+�5   
   �� -   5        $��New Game��5      
������$
��Left=�6<   $��366,283,0,0q�6W   $	��Top=�6l   $��80��6�   $��27��6�   6�   o� 5   
   $��29��6   $	��Top=�6   
�������.@�5      m� $
��File��6   $��28��6%   $��40��61   
�������.@�5      m� -   5        $��New Game��5      
�������6W   m� 5      -   5        $
��Exit��5      
�������6V   5      5       �� 5"   
   -   5         ��Name$ ship4_0��5"      $��25��6!   $	��100��6-   $��
375,67,0,0q�69   $��Resources/4DeckShip.gif��6R   6�   �� 5#   
   -   5        $ ship3_0��5#      $��25��6!   $��75��6.   $��400,101,321,226q�69   $��Resources/3DeckShip.gif��6R   6�   �� 5$   
   -   5        $ ship3_1��5$      $��25��6!   $��75��6.   $��400,101,321,226q�69   $��Resources/3DeckShip.gif��6R   6�   �� 5%   
   -   5        $ ship2_0��5%      $��25��6!   $��50��6.   $��424,130,322,197q�69   $��Resources/2DeckShip.gif��6R   6�   �� 5&   
   -	   5        $ ship2_1��5&      $��25��6!   $��50��6.   $��424,130,322,197q�69   $��Resources/2DeckShip.gif��6R   6�   �� 5'   
   -
   5        $ ship2_2��5'      $��25��6!   $��50��6.   $��424,130,322,197q�69   $��Resources/2DeckShip.gif��6R   6�   �� 5(   
   -   5        $ ship1_0��5(      $��25��6!   $��25��6.   $��450,161,321,166q�69   $��Resources/1DeckShip.gif��6R   6�   �� 5)   
   -   5        $ ship1_1��5)      $��25��6!   $��25��6.   $��450,161,321,166q�69   $��Resources/1DeckShip.gif��6R   6�   �� 5*   
   -   5        $ ship1_2��5*      $��25��6!   $��25��6.   $��450,161,321,166q�69   $��Resources/1DeckShip.gif��6R   6�   �� 5+   
   -   5        $ ship1_3��5+      $��25��6!   $��25��6.   $��450,161,321,166q�69   $��Resources/1DeckShip.gif��6R   6�   �� 5,   
   -   5        $ mDesk��5,      $
��Left=�6    $	��250��6;   $��	33,52,0,0q�6H   $	��Top=�6\   $	��250��6t   �� 5-   
   -   5        $ fDesk��5-      $
��Left=�6    $	��250��6;   $��
535,52,0,0q�6H   $	��Top=�6\   $	��250��6t   �� 5.   
   -   5         ��Name$ slider��5.      	 �� Left$		 360��6!   

 �� Top$	
 318��63   $��87��6D    � Value$ 30Q�6O    � SmallChange$ 1Q�6Z   �� 5/   
   -   5         ��Name$ labelSlider��5/      $!��Сложность бота��6&   $��24��6?   $		 253��6K   $	
 313��6]   $	��107��6n    �� ContentStringFormat$  ��6z   50      52      