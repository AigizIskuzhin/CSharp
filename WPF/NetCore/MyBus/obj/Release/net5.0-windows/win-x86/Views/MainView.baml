   M S B A M L   `   `   `  ���� @  <MyBus, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:MyBus.Models.UserMyBus.Models.User      MyBus.Views.MainView   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     0userclr-namespace:MyBus.Models.User     -     >�Name$
  view�� � DataContext �� *MainViewModel+   Locator �� Source# [  .���  � RowDefinitions �� $
��Auto ��� �� $
��Auto �.+��  �� �� .+���  �� Orientation$ 
Horizontal=�$��0��$��Bottom=�$��Center=�.+��� $��
Поиск��$��0 0 10 0q��� $	��250��$��Center=����� *UsersFilterText+ �� UpdateSourceTrigger$ PropertyChanged=�� $��1��.+�� .+��� $	��100������ *FiltersDictionary+���� .R�� �� $��Add Filter Category������ *AddFilterCategoryCommand+�� $��1��$��Stretch=� �� SelectedItem �� *SelectedUser+���� *	UsersView+���� .R���  �� Padding$ 4q�$��1q�
�������._���  � 	Resources 	 �� 
TargetType
 �� Triggers P� Property P� Value �� Property �� Value��%N   '��       �� "	 ��
 P�  � �� Z� . ���  � y�$ 	Collapsed=�.+��  �� ��  � ColumnDefinitions �� $
��Auto ��� �� $
��Auto �.+��� $��0,0,10,0q�.+��� ���� *ID+ �� StringFormat$ Код - {0}���� ���� *Phone+$ Телефон - {0}���� $��1��  �� Value   MyBus.Models.User.Client �� Binding��%+   '��       �� "	 ��
 �� "   �� .+��� ���� *Name+$ Имя - {0}���� ���� *	Surname+$ Фамилия - {0}���� ���� *
Patronymic+$ Отчество - {0}���� ���� *
Birthday+�� $ 
Horizontal=�$��1��$��2��.+��� ���� �� $��10,0,0,0q����� *Level+�� $��0��$��2��$ 
Horizontal=�$	��Top=�$��Right=�.+���   
BtnEditLoc#���   ��%r   '��       �� "	 ��.���  � ��
 �����
 P�  �@J�IsMouseOver$
 True��. ���  � ��
 �  �� J� InputBindings d�  d� MouseAction$ 	LeftClicke� �� Command ��  �� ElementName$
 view�� �� Path$! DataContext.EditUserCommand� �� CommandParameter �� A�  A�Name$ delete_bg_circle��$��20��$��15����!�  !� Visual �� $��25��$��X�� ��%�   '��       �� "	 ��.���  � ��
 �����
 ��  �� $ delete_bg_circle��$ IsMouseOver� True.a���  � ��
 �  �� d� $ 	LeftClicke� �� $
 view��$# DataContext.DeleteUserCommand� �� � $��2�� �� $
��Auto ��� .+��� $��14(�$��Center=�$
��10,2q����� *UsersView.Count+$ {0} записей���� $��1��$ 
Horizontal=�$��Right=�.+���   	BtnAddLoc#���  �� #���  ���� *EditSelectedUserCommand+���� *SelectedUser+��   BtnDeleteLoc#���  