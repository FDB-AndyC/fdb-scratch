VERSION 1.0 CLASS
BEGIN
  MultiUse = -1  'True
END
Attribute VB_Name = "ThisOutlookSession"
Attribute VB_GlobalNameSpace = False
Attribute VB_Creatable = False
Attribute VB_PredeclaredId = True
Attribute VB_Exposed = True
Public Sub ReplaceAttachmentsToLink()
Dim objApp As Outlook.Application
Dim aMail As Outlook.MailItem 'Object
Dim oAttachments As Outlook.Attachments
Dim oSelection As Outlook.Selection
Dim i As Long
Dim iCount As Long
Dim sFile As String
Dim sFolderPath As String
Dim sExtension As String
Dim sDeletedFiles As String
Dim oFso
Dim iDeletedAttachments As Integer
  
    ' Get the path to your My Documents folder
    sFolderPath = CreateObject("WScript.Shell").SpecialFolders(16)
    On Error Resume Next
  
    ' Instantiate an Outlook Application object.
    Set objApp = CreateObject("Outlook.Application")
  
    ' Get the collection of selected objects.
    Set oSelection = objApp.ActiveExplorer.Selection
  
    ' Set the Attachment folder.
    sFolderPath = sFolderPath & "\OLAttachments"
  
    Set oFso = CreateObject("Scripting.FileSystemObject")
    iDeletedAttachments = 0
     
    ' Check each selected item for attachments. If attachments exist,
    ' save them to the Temp folder and strip them from the item.
    For Each aMail In oSelection
  
    ' This code only strips attachments from mail items.
    ' If aMail.class=olMail Then
    ' Get the Attachments collection of the item.
    Set oAttachments = aMail.Attachments
    iCount = oAttachments.Count
        
    If iCount > 0 Then
      
        ' We need to use a count down loop for removing items
        ' from a collection. Otherwise, the loop counter gets
        ' confused and only every other item is removed.
          
        For i = iCount To 1 Step -1
          
            ' Save attachment before deleting from item.
            ' Get the file name.
            sFile = oAttachments.Item(i).FileName
            sExtension = LCase(oFso.GetExtensionName(sFile))
            
            If (sExtension = "pdf" Or sExtension = "docx" Or sExtension = "doc") Then
                ' Combine with the path to the Temp folder.
                sFile = sFolderPath & "\" & sFile
                  
                ' Save the attachment as a file.
                oAttachments.Item(i).SaveAsFile sFile
                  
                ' Delete the attachment.
                oAttachments.Item(i).Delete
                iDeletedAttachments = iDeletedAttachments + 1
                  
                'write the save as path to a string to add to the message
                'check for html and use html tags in link
                If aMail.BodyFormat <> olFormatHTML Then
                    sDeletedFiles = sDeletedFiles & vbCrLf & "<file://" & sFile & ">"
                Else
                    sDeletedFiles = sDeletedFiles & "<br>" & "<a href='file://" & _
                    sFile & "'>" & sFile & "</a>"
                End If
                  
            End If
        
                          
        Next i
        'End If
              
       ' Adds the filename string to the message body and save it
       ' Check for HTML body
       If aMail.BodyFormat <> olFormatHTML Then
           aMail.Body = aMail.Body & vbCrLf & _
           "The file(s) were saved to " & sDeletedFiles
       Else
           aMail.HTMLBody = aMail.HTMLBody & "<p>" & _
           "The file(s) were saved to " & sDeletedFiles & "</p>"
       End If
        
       aMail.Save
       'sets the attachment path to nothing before it moves on to the next message.
       sDeletedFiles = ""
     
       End If
    Next 'end aMail
    
MsgBox "Deleted " + CStr(iDeletedAttachments) + " attachment(s)", vbOKOnly, "Delete summary"
      
ExitSub:
  
Set oAttachments = Nothing
Set aMail = Nothing
Set oSelection = Nothing
Set objApp = Nothing
End Sub

