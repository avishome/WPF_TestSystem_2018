   M S B A M L   `   `   `  ����=  9UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:UIUI  5   	       UI.NewStudent   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:UI     5   	     � Title$  
NewStudent��5   	   $	��500��6   $	��450��6)   .��5	      �� $��Disabled=�6   .��5
   
   �� -   5         ��Name$ 
stackPanel��5
      .+�5      � $��10q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'    � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   �� 5       $
��Auto �6$   .+�5"      �� -   5        $��console��5"      $��2��6'   69   �� 5#      -   5        $��console1��5#   %   $��1��6   $��2��65   6G   �� 5$      -   5        $��console2��5$   %   $��2��6   $��2��65   6G   �� 5%      -   5        $��console3��5%   %   $��3��6   $��2��65   6G   �� 5&      -   5        $��console4��5&   %   $��4��6   $��2��65   6G   �� 5'      -   5        $��console5��5'   %   $��5��6   $��2��65   6G   �� 5(      -   5        $��console6��5(   %   $��6��6   $��2��65   6G   �� 5)      -	   5        $��console7��5)   %   $��7��6   $��2��65   6G   �� 5*      -
   5        $��console8��5*   %   $��8��6   $��2��65   6G   �� 5+      -   5        $��console9��5+   %   $��9��6   $��2��65   6G   �� 5,      -   5        $��	console10��5,   &   $��10��6   $��2��67   6I   �� 5-      -   5        $��	console11��5-   &   $��11��6   $��2��67   6I   �� 5.      -   5        $��	console12��5.   &   $��12��6   $��2��67   6I   �� 5/      -   5        $��	console13��5/   &   $��13��6   $��2��67   6I   �� 50      .��6   Id:6   �� 51      -   5        $��
IntefaceId��51      $��1��6q   $��0,0,0,10q�6�   ��6,   ��  �� Path$ Id� �� UpdateSourceTrigger$ 	LostFocus=� �� Mode$ TwoWay=�6�   �� 52      -   5        $��
IntefaceFN��52      $��1��6x   $��1��6�   $��0,0,0,10q�6�   ��6,   �� $ 	FirstName�$ 	LostFocus=�$ TwoWay=�6�   �� 53      -   5        $��
IntefaceLN��53      $��2��6w   $��1��6�   $��0,0,0,10q�6�   ��6,   �� $ LastName�$ 	LostFocus=�$ TwoWay=�6�   �� 54      -   5        $��
IntefaceAC��54      $��4��6,   $��1��69   $��0,0,0,10q�6I   �� 55      -   5        $��
IntefaceAS��55      $��5��6,   $��1��69   $��0,0,0,10q�6I   �� 56      -   5        $��GenderSelect��56      $��3��6/   $��1��6<   �� 57      -   5        $��
IntefaceAN��57      $��6��6,   $��1��69   $��0,0,0,10q�6I   �� 58      -   5        $��
IntefacePN��58      $��7��6z   $��1��6�   $��0,0,0,10q�6�   ��6,   �� $ PhoneNumber�$ 	LostFocus=�$ TwoWay=�6�   (  "System.Windows.Controls.DatePicker  59      -   5        $��
IntefaceBD��59   8   $
��Left=�6   $��8��6J   $��1��6W   $	��Top=�6g   $��0,0,0,10q�6      SelectedDate 6�   �� $ BirthDay� �� StringFormat$ {0:dd-MM-yyyy}��	 � 	Resources	 5:      :  4System.Windows.Controls.Primitives.DatePickerTextBox
 �� 
TargetType �� Property �� Value ��Name �� RelativeSource �� AncestorType��%�   '        �� 5;      "
  6    .�5<      ��  � �� 5=   "   �� 5>   &   .R�5?   *   �� $ PART_TextBox��62   ��6H   �� $ SelectedDate�$ {0:dd-MM-yyyy}�� �� "  $ TwoWay=�$ 	LostFocus=�5@   '   5B      5C      5D      5E      �� 5F      -   5        $
��Gear��5F      $��9��6'   $��1��64   �� 5G      -   5        $	��Car��5G      $��10��6&   $��1��64   �� 5H      -   5        $��IntefaceHoursLearned��5H      $��11��6�   $��1��6�   $��0,0,0,10q�6�   ��66   �� $ HoursLearned�$ 	LostFocus=�$ TwoWay=�6�   �� 5I      -   5        $��IntefaceTeacherName��5I      $��12��6�   $��1��6�   $��0,0,0,10q�6�   ��65   �� $ TeacherName�$ 	LostFocus=�$ TwoWay=�6�   �� 5J      -   5        $��IntefaceSchoolName��5J      $��13��6�   $��1��6�   $��0,0,0,10q�6�   ��64   �� $ 
SchoolName�$ 	LostFocus=�$ TwoWay=�6�   �� 5L      $��1��6   .��6%   first name:62   �� 5M      $��2��6   .��6%   
last name:61   �� 5N      $��3��6   .��6%   	gender:6.   �� 5O      $��4��6   .��6%   addres City:63   �� 5P      $��5��6   .��6%   addres Street:65   �� 5Q      $��6��6   .��6%   addres Number:65   �� 5R      $��7��6   .��6%   phone number:64   �� 5S      $��8��6   .��6%   
birth day:61   �� 5T      $��9��6   .��6%   type of gear:64   �� 5U      $��10��6   .��6&   type of car:64   �� 5V      $��11��6   .��6&   HoursLearned:65   �� 5W      $��12��6   .��6&   TeacherName:64   �� 5X      $��13��6   .��6&   SchoolName:63   �� 5[      -   5        $��14��5[      $��1��6'   $
��Send��6L   5]      5^      5_      5`      