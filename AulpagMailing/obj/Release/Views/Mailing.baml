   M S B A M L   `   `   `  ���� H  DAulpagMailing, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:AulpagMailing.HelpersAulpagMailing.Helpers  ?$clr-namespace:AulpagMailing.ControlsAulpagMailing.Controls  !    AulpagMailing.Views.Mailing   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     c _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35Bi:http://schemas.microsoft.com/expression/2010/interactivity  1Helpers#clr-namespace:AulpagMailing.Helpers  X TXceed.Wpf.Toolkit, Version=4.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f47toolkit)http://schemas.xceed.com/wpf/xaml/toolkit  /util$clr-namespace:AulpagMailing.Controls  b ^GalaSoft.MvvmLight.Platform, Version=5.4.1.0, Culture=neutral, PublicKeyToken=5f873c45e98af8a1.command http://www.galasoft.ch/mvvmlight    � WindowStartupLocation$  CenterScreen=� � Title$ Mailing��$	��700��$
��1309��.  (System.Windows.Interactivity.Interaction   	Behaviors /   )AulpagMailing.Helpers.CloseWindowBehavior     CloseTrigger �� *CloseTrigger+ � 	Resources -  'AulpagMailing.Helpers.AdherentConverter   adherent.   (AulpagMailing.Helpers.CategorieConverter  	categorie��%   &	        &	             Triggers /  )System.Windows.Interactivity.EventTrigger     	EventName$ MouseDoubleClick��.  (System.Windows.Interactivity.TriggerBase   Actions. 6  0System.Windows.Interactivity.InvokeCommandAction     Command �� *DeletePieceCommand+	   CommandParameter	 �� 
 �� ElementName$
 TvBox�� �� Path$ SelectedItem�  $ Closing��. /  )GalaSoft.MvvmLight.Command.EventToCommand     PassEventArgsToCommand .    Command �� *WindowClosing+.���� -    ��Name$ 
tabControl�� �� SelectedIndex �� *SelectedIndex+.@��� $��Courriel��$��0,-2,-100,0q�.��� .+��   �� 
TargetType  formatTextStyle �� Property �� Value  formatImageStyle  formatParagraphe��%%  &	       &	 �     &	 �     �� " ��.���  � ��$ Palatino Linotype)���  � ��$ 30����  � ��$ 14(���  � �� �� $
 mainRTB����  � ��$ 0q��� " ��.���  � ��$ 30����  � �� �� $
 mainRTB���� " J�.���  �@��Margin$ 0q� � RowDefinitions �� $��30 ��� $��30 ��� $��30 ��� $��* ��� $	��100 ��� $��30 � � ColumnDefinitions �� $	��130 ��� �� $	��200 �.+��� $��1�� �� Orientation$ 
Horizontal=�.+�	� $��5q�$��Courriel�� d� 	IsChecked �� *CourielChecked+	� $��5q�$	��SMS�� �� *
SmsChecked+�� $��67,0,5,0q�$��1��$��Objet:���� $��0,5,10,5q�$��1��$��1��$��2������ *CurrentMailing.objet_mailing+�� 
����+��$��1q�$��2��$��1��$��2��$��0,0,10,0q�._�c� -   $��mainToolBar��$��2��$��1��$	��Top=�$��0,0,0,0q�.@��� -    ��Name$ FontFamilyCombo��$��23��$	��100��$��5,2,0,2q� �� 
IsEditable . ���� .R�$� $	��250������ .R��� $��10(�$��20������ ���� �� -   $ FontSizeCombo��$��23��$��40��$��5,2,5,2q� . �� #��[ $��ApplicationCommands.Cut�� � ToolTip$	 Cut��.���� $��/Images/Editcut.png���� #��[ $��ApplicationCommands.Copy��$
 Copy��.���� $��/Images/EditCopy.png���� #��[ $��ApplicationCommands.Paste��$ Paste��.���� $��/Images/EditPaste.png���� #��[ $��ApplicationCommands.Undo��$
 Undo��.���� $��/Images/EditUndo.png���� #��[ $��ApplicationCommands.Redo��$
 Redo��.���� $��/Images/EditRedo.png���� #��[ $ ��EditingCommands.ToggleBold��$
 Bold��.���� $
��Bold"��� $�B���� #��[ $"��EditingCommands.ToggleItalic��$ Italic��.���� $��Italic$�$
��Bold"��� $�I���� #��[ $%��EditingCommands.ToggleUnderline��$ 	Underline��.���� $��	Underline{�$
��Bold"��� $�U���� #��[ $&�� EditingCommands.IncreaseFontSize��$ 	Grow Font��.���� $#��/Images/CharacterGrowFont.png���� #��[ $&�� EditingCommands.DecreaseFontSize��$ Shrink Font��.���� $%��/Images/CharacterShrinkFont.png���� #��[ $#��EditingCommands.ToggleBullets��$ Bullets��.���� $��/Images/ListBullets.png���� #��[ $%��EditingCommands.ToggleNumbering��$ 	Numbering��.���� $��/Images/ListNumbering.png���� #��[ $��EditingCommands.AlignLeft��$ 
Align Left��.���� $&�� /Images/ParagraphLeftJustify.png���� #��[ $!��EditingCommands.AlignCenter��$ Align Center��.���� $(��"/Images/ParagraphCenterJustify.png���� $ ��EditingCommands.AlignRight��$ Align Right��.���� $'��!/Images/ParagraphRightJustify.png���� #��[ $"��EditingCommands.AlignJustify��$ Align Justify��.���� $&�� /Images/ParagraphFullJustify.png���� #��[ $)��#EditingCommands.IncreaseIndentation��$ Increase Indent��.���� $.��(/Images/ParagraphIncreaseIndentation.png���� #��[ $)��#EditingCommands.DecreaseIndentation��$ Decrease Indent��.���� $.��(/Images/ParagraphDecreaseIndentation.png��#	  Xceed.Wpf.Toolkit.RichTextBox	  -    	 Name$ TextBox��$��3��$��1��
�������$
��Auto=� J� RenderTransformOrigin$ 
0.512,0.61&� 	  Text �� *CurrentMailing.contenu+�� $��3��$��0��.+��� $��
67,10,5,10q�$��3��$��Sujet:��$��90���� $��
20,0,20,20q�
�������
����������� *ListCommand+.���� .+��� $��/Images/List-Icon.svg.png��$��40���� �� $�Liste documents���� $��
20,0,20,20q�
�������
����������� *SendCommand+.���� .+��� $"��/Images/envoiCourriel-96.png��$��40���� �� $�Envoyer���� $��
20,0,20,20q�
�������
����������� *CloseWindowCommand+.���� .+��� $��/Images/fermer-64.png��$��40���� �� $�Quitter���� -    ��Name$ 
debugPanel��
����+��$��1q�$��4��$��1��$��2��$��0,0,10,0q�._��� $��4��$��1��$ 
Horizontal=�$��0,0,4,0q�.+��� $��10,0,0,0q�
�������
����������� *SignCommand+.���� $ 
Horizontal=�.+��� $��/Images/signature.png��$��25���� $
��10,0q��� $�	Signature���� ���� *CurrentMailing.signature+�� $��600,0,100,0q�
�������
����������� *	PjCommand+.���� $ 
Horizontal=�.+��� $��/Images/piece_jointe.png��$��25���� �� $�Pièces jointes���� -   $��TvBox��$��4��$��2��$��0,0,10,0q�$��Visible=���.  $��Stretch=�$��Stretch=����� *PiecesJointes+���� " ��.���  � ��$	 Top=���  � ��$ Stretch=� �� View �� ! �� ColumnHeaderContainerStyle �� " ��.���  � y�$ 	Collapsed=�.P���   �� Width$	  130��! �� DisplayMemberBinding! �� *piece+�� $  30��" �� CellTemplate" �� .R��� $	��Bt1��
�������$  Supprimer la pièce jointe��.���� $��Center=�$��Center=�$��x���� $��2��$��2��$��Destinataires��$��	42,5,38,0q��� 
����+��$	��0.5q�$��3��$��3��$��0,0,10,0q�._��� -   # ��Name$# lvUsers3��$��Visible=���.  ���� *	ListEnvoi+���� " ��.���  � ��$	 Top=���  � ��$ Stretch=� �� .P��� $	  150��$��Email��! �� *email+�� $��Destinataires��$��100,-2,-200,0q�$ 0.158,0.682&�.��� .+��� .+��� $��	Recherche��$��10,10,1084,10q�$��10q�.���� ���� *Bt.Recherche+$ �� Mode$$ TwoWay=�% �� UpdateSourceTrigger$% PropertyChanged=��� $��Pointage��$��10,10,1084,10q�$��10q�.���� .+��� $��Sélectionner tout��$
��Left=�$	��155��$��1,0,0,0q����� *ReselectionCommand+�� $��Délectionner tout��$
��Left=�$	��155��$��1,0,0,0q����� *DeselectionCommand+�� $��Catégories��$��10,10,1084,10q�$��10q�.���� $
��Left=�$	��167��$��0,0,-7,0q�.+��� $��Usagers�� �� *	Bt.Usager+$$ TwoWay=��� $��Personalités�� �� *Bt.Personalite+$$ TwoWay=��� $��Presse�� �� *	Bt.Presse+$$ TwoWay=��� $��
Adhérents��$��10,10,1079,10q�$��10q�.���� .+��� $��Inscrits�� �� *
Bt.Inscrit+�� $��Non inscrits�� �� *Bt.N_inscrit+�� $��Fiches sélectionnées��$��10,10,1079,10q�$��10q�.���� .+��� $��Filtrer�� �� *Bt.Fiche_Selectionnes+�� $��Fiches��$��10,10,1079,10q�$��10q�.���� .+��� $��Ajouter��$
��Left=�$	��155��$��1,0,0,0q����� *NewDestinataireCommand+�� $��Importer��$
��Left=�$	��155��$��1,0,0,0q����� *ImportToCsvCommand+�� $��Exporter��$
��Left=�$	��155��$��1,0,0,0q����� *ExportToCsvCommand+�� $��217,20,175,16q�.+��� -	   $# lvUsers��$��Visible=���.  $	��550��$��Stretch=�$��Stretch=�)
   #AulpagMailing.Controls.GridViewSort& 
  AutoSort& . $	��550������ *ListDestinataires+   $ MouseDoubleClick��.    �� *ModifFicheDestinataire+	 �� $
 lvUsers��$ SelectedItem����� " ��.���  � ��$	 Top=���  � ��$ Stretch=� �� .P��� $�� ��" �� .R���  �� *
selected+�� $	  100��$��Catégories��' 
  PropertyName$' 	categorie��" �� .R��� ( �� TextAlignment$( Center=����� *	categorie+) �� 	Converter#) [ �� $	  100��$��	Adhérent��$' adherent��" �� .R��� $( Center=����� *
adherent+#) [  �� $  50��$��	Civilité��! �� *	civilité+�� $	  150��$	��Nom��$	' nom��! �� *nom+�� $	  100��$��Prénom��$' prenom��! �� *prenom+�� $	  200��$��E-mail��! �� *email+�� $	  100��$��Titre��! �� *titre+