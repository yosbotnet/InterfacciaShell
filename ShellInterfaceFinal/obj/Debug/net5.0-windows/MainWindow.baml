   M S B A M L   `   `   `  ����N  JShellInterfaceFinal, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:ShellInterfaceFinalShellInterfaceFinal  5   	   $    ShellInterfaceFinal.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:ShellInterfaceFinal     5   	     � Title$  Shell��5   	   $	��515��6   $	��977��6$   .��5	      �  � RowDefinitions 5
   
   �� 5      $	��91* �6   �� 5      $
��328* �6   .+�5   
   �� -   5         ��Name$ 
btnAscolta��5      $��Ascolta��6%   $
��Left=�67   $��
158,40,0,0q�6R   $	��Top=�6f   $��75��6~   6�   �� 5   
   -   5         ��Name$ txtPortaAscolto��5      $
��Left=�6+   $��20��6F   $��	25,40,0,0q�6R    �� TextWrapping$
 Wrap=�6e   $��Porta��6y   $	��Top=�6�   $	��120��6�   �� 5   
   -   5         ��Name$ 	lblPrompt��5      $
��C:\>��6#   $
��Left=�62   $��
305,40,0,0q�6M   $	��Top=�6a   $	��250��6y   $��2��6�   $	��611��6�   �� 5   
   -   5        $ 
txtComando��5      $
��Left=�6(   $��23��6C   $��305,212,0,0q�6O   $��1��6d   $
 Wrap=�6q   $	��dir��6�   $	��Top=�6�   $	��509��6�   �� 5   
   -   5        $ btnInvia��5      $��Invia��6#   $
��Left=�63   $��819,212,0,0q�6N   $��1��6c   $	��Top=�6p   $��97��6�   $��23��6�   6�   �� 5   
   -   5        $ txtIP��5      $
��Left=�6!   $��23��6<   $��	25,18,0,0q�6H   $��1��6[   $
 Wrap=�6h   $��	127.0.0.1��6|   $	��Top=�6�   $	��105��6�   �� 5   
   -   5        $ txtPorta��5      $
��Left=�6$   $��23��6?   $��
144,18,0,0q�6K   $��1��6_   $
 Wrap=�6l   $
��4444��6�   $	��Top=�6�   $��89��6�   �� 5   
   -   5        $ 
btnConnect��5      $��Collega��6%   $
��Left=�67   $��	25,69,0,0q�6R   $��1��6e   $	��Top=�6r   $	��208��6�   6�   �� 5   
   $��:��6   $
��Left=�6   $��
130,16,0,0q�67   $��1��6K   $	��Top=�6X   $��14��6p   �� 5   
   -	   5        $ btnDisconnect��5      ${�False��6(   $��Disconnetti��6:   $
��Left=�6P   $��
25,123,0,0q�6k   $��1��6   $	��Top=�6�   $	��208��6�   6�   5      5      