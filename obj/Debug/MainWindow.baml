   M S B A M L   `   `   `  ����D  @SuperList, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:SuperList	SuperList  5   	   -clr-namespace:SuperList.WPFSuperList.WPF  5   	   O KSuperList.ViewModels, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY@clr-namespace:SuperList.ViewModels;assembly=SuperList.ViewModelsSuperList.ViewModels 5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       SuperList.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   -	superlistclr-namespace:SuperList     5   	   -localclr-namespace:SuperList.WPF     5   	   Ovm@clr-namespace:SuperList.ViewModels;assembly=SuperList.ViewModels     5   	   6sys&clr-namespace:System;assembly=mscorlib     5   	     � Title$$  Medicity Ticket Tracker v. 1.0��5   	   $	��500��60   $	��820��6=    � 	Resources 5
      !  SuperList.WPF.DateConverter   DateConverter��%   &	          5   
   6   5       � DataContext 5      -  'SuperList.ViewModels.SuperListViewModel  5   
   -   5        	SuperList��5       .��5      �  � RowDefinitions 5   
   �� 5      $��30 �6   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   � $��1��6   $��Stretch=�6   $��Stretch=�6:    � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5      $��* �6#   .+�5      �� -   5         ��Name$ MasterListBox��5   D   $��0��5       $��Stretch=�5!      $��Stretch=�5"      $��0 0 3 0q�5#       �� SelectionMode$ Single=�5$      ��5      �� *
MainList+ �� Mode$ TwoWay=� �� SelectedItem 5%      �� *CurrentlySelectedList+��5'      �� 5(      .R�5)      �� 	 �� Orientation$	 
Horizontal=�6%   $��Center=�6>   .+�5*      �� $��X��6%   $��5q�61   $��
3, 2, 0, 2q�5-   %   ��5+   %   �� 
 �� RelativeSource
 ��  �� Mode$
 Self=���5,   %   �� *#!DataContext.RemoveToDoListCommand+
 �� $ FindAncestor=� �� AncestorType" ��5-   %   �� 5/      $��5q�50   &   $	��130��51   &   ��5/   &   �� *Name+51   &   52      53      ��56      �� 57       �� 
TargetType" ��6    �� Triggers 58      P� 59       P� Property � {� P� Value . . �5:   "   ��  �� Property �@��
IsSelected �� Value . 6?   5;      5=      5?      &   System.Windows.Controls.DataGrid  5A      -   5          Name$ 	ItemsGrid��5A      $��Stretch=�5B      $��Stretch=�5C         AutoGenerateColumns .     CanUserAddRows .    CanUserDeleteRows . "   VerticalScrollBarVisibility$
 Auto=�5G      $   HorizontalScrollBarVisibility$
 Auto=�5H         SelectionMode$ Single=�5I         SelectionUnit$ FullRow=�5J         RowHeaderWidth$ 0Q�5K      $��1��5N   -    5L      ��  �� Path$ Items.SelectedItem� 5M      ��  �� ElementName$ MasterListBox��$ SelectedItem���5O      �� $ Items�   Columns 5Q      4  .System.Windows.Controls.DataGridTemplateColumn  5R      ,  &System.Windows.Controls.DataGridColumn   Header$ Due Date��6-       CellTemplate  5S      �� 5T      .R�5U   "   (  "System.Windows.Controls.DatePicker  !   SelectedDate! 6-   �� $ DueDate�5V      5X      0  *System.Windows.Controls.DataGridTextColumn  5Y      $ Heat Ticket��6)   1  +System.Windows.Controls.DataGridBoundColumn"   Binding" 6>   �� $ 
HeatTicket�$ TwoWay=�  5Z      $ Item Description��6)   #   Width5	  /System.Windows.Controls.DataGridLengthConverter$	# 175	 6y   " 6C   �� $ ItemDescription�$ TwoWay=�$   ElementStyle$ 5[      �� 5\      " ��6$   .�5]   "   ��  � ��$
 Wrap=�6A   �� 5^   "    �@��TextAlignment$ Center=�6B   5_      5a        5b      $ Item Created Date��6)   " 6D   �� $ 
CreateDate�% �� 	Converter#% [  $ OneWay=�4
  .System.Windows.Controls.DataGridCheckBoxColumn
  5c      $ Item Complete��6-   " 6D   �� $ 
IsComplete�  5d      $ Delete��6-     5e      �� 5f      .R�5g   "   �� $��X��6)   $��Delete��65   $��15��6F   5h      5j      5m      5o      �� 5q   
   $��0��5r      
���   �$��Center=�5t      $��Center=�5u      $��16(�5v      $
��Bold"�5w      ��5q      �� *SelectedItem.Name+$ MasterListBox��5w      �� 5y   
   $	 
Horizontal=�6   $��2��6.   .+�5z      �� $��Create New List��6   $��	10,0,10,0q�5{      $
��10,5q�5|      ��5}      �� $ AddToDoListCommand��� 5~      $2��,Double-click below an item to add a new one.��6   $��
250,0,20,0q�5      $��Center=�5�      �� 5�      $
��Save��6   $��	10,0,10,0q�5�      $��60,5,6,5q�5�      ��5�      �� *SaveFileCommand+�� 5�      $
��Load��6   $��	10,0,10,0q�5�      $��3,5,10,5q�5�      ��5�      �� *LoadFileCommand+5�      5�      5�      