   M S B A M L   `   `   `  ����=  9UI, Version=0.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:UIUI  5   	       UI.NewTeacher   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   "localclr-namespace:UI     5   	   -   5          � Title$  
NewTeacher��5   	   $	��300��6   $	��450��6)   .��5
      �� $��Disabled=�6   .��5   
   �� -   5         ��Name$ 
stackPanel��5      .+�5      � $��10q�6    � ColumnDefinitions 5      �� 5      $
��Auto �6'   �� 5      $
��Auto �6'   �� 5      $
��Auto �6'    � RowDefinitions 5      �� 5      $
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
��Auto �6$   �� 5!      $
��Auto �6$   �� 5"      $
��Auto �6$   .+�5%      �� -   5        $��console��5%      $��2��6'   69   �� 5&      -   5        $��console1��5&   %   $��1��6   $��2��65   6G   �� 5'      -   5        $��console2��5'   %   $��2��6   $��2��65   6G   �� 5(      -   5        $��console3��5(   %   $��3��6   $��2��65   6G   �� 5)      -   5        $��console4��5)   %   $��4��6   $��2��65   6G   �� 5*      -   5        $��console5��5*   %   $��5��6   $��2��65   6G   �� 5+      -	   5        $��console6��5+   %   $��6��6   $��2��65   6G   �� 5,      -
   5        $��console7��5,   %   $��7��6   $��2��65   6G   �� 5-      -   5        $��console8��5-   %   $��8��6   $��2��65   6G   �� 5.      .��6   Id:6   �� 5/      -   5        $��
IntefaceId��5/      $��1��6q   $��0,0,0,10q�6�   ��6,   ��  �� Path$ Id� �� UpdateSourceTrigger$ 	LostFocus=� �� Mode$ TwoWay=�6�   �� 50      $��1��6   .��6%   first name:62   �� 51      -   5        $��
IntefaceFN��51      $��1��6x   $��1��6�   $��0,0,0,10q�6�   ��6,   �� $ 	FirstName�$ 	LostFocus=�$ TwoWay=�6�   �� 52      $��2��6   .��6%   
last name:61   �� 53      -   5        $��
IntefaceLN��53      $��2��6w   $��1��6�   $��0,0,0,10q�6�   ��6,   �� $ LastName�$ 	LostFocus=�$ TwoWay=�6�   �� 54      $��3��6   .��6%   	gender:6.   �� 55      -   5        $��GenderSelect��55      $��3��6/   $��1��6<   �� 57      $��4��6   .��6%   addres City:63   �� 58      -   5        $��
IntefaceAC��58      $��4��6,   $��1��69   $��0,0,0,10q�6I   �� 59      $��5��6   .��6%   addres Street:65   �� 5:      -   5        $��
IntefaceAS��5:      $��5��6,   $��1��69   $��0,0,0,10q�6I   �� 5;      $��6��6   .��6%   addres Number:65   �� 5<      -   5        $��
IntefaceAN��5<      $��6��6,   $��1��69   $��0,0,0,10q�6I   �� 5=      $��7��6   .��6%   phone number:64   �� 5>      -   5        $��
IntefacePN��5>      $��7��6z   $��1��6�   $��0,0,0,10q�6�   ��6,   �� $ PhoneNumber�$ 	LostFocus=�$ TwoWay=�6�   �� 5?      $��8��6   .��6%   
birth day:61   (  "System.Windows.Controls.DatePicker  5A      -   5        $��
IntefaceBD��5A   8   $
��Left=�6   $��8��6J   $��1��6W   $	��Top=�6g   $��0,0,0,10q�6      SelectedDate 6�   �� $ BirthDay� �� StringFormat$ {0:dd-MM-yyyy}��	 � 	Resources	 5B      :  4System.Windows.Controls.Primitives.DatePickerTextBox
 �� 
TargetType �� Property �� Value ��Name �� RelativeSource �� AncestorType��%�   '        �� 5C      "
  6    .�5D      ��  � �� 5E   "   �� 5F   &   .R�5G   *   �� $ PART_TextBox��62   ��6H   �� $ SelectedDate�$ {0:dd-MM-yyyy}�� �� "  $ TwoWay=�$ 	LostFocus=�5H   '   5J      5K      5L      5M      �� 5N      $��9��6   .��6%   type of gear:64   �� 5O      -   5        $
��Gear��5O      $��9��6'   $��1��64   �� 5P      $��10��6   .��6&   type of car:64   �� 5Q      -   5        $	��Car��5Q      $��10��6&   $��1��64   �� 5R      $��11��6   .��6&   max hours per week:6;   �� 5S      $��12��6   .��6&   max KM from home:69   �� 5T      $��13��6   .��6&   seniority years:68   �� 5U      -   5        $��
IntefaceHR��5U      $��11��6~   $��1��6�   $��0,0,0,10q�6�   ��6,   �� $ maxHoursPerWeek�$ 	LostFocus=�$ TwoWay=�6�   �� 5V      -   5        $��
IntefaceKM��5V      $��12��6|   $��1��6�   $��0,0,0,10q�6�   ��6,   �� $ maxKMFromHome�$ 	LostFocus=�$ TwoWay=�6�   �� 5W      -   5        $��
IntefaceSN��5W      $��13��6}   $��1��6�   $��0,0,0,10q�6�   ��6,   �� $ SeniorityYears�$ 	LostFocus=�$ TwoWay=�6�   �� 5Y      $��14��6   .��6&   	schedule:61   �� 5Z      -   5        $��14��5Z      $��1��6'   $��schdule��6N   �� 5[      -   5        $��15��5[      $��1��6'   $
��Send��6L   5]      5^      5_      5`      