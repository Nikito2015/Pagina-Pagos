<%@LANGUAGE="VBSCRIPT" %>
<!--METADATA TYPE="TypeLib" FILE="C:\WINDOWS\system32\cdosys.dll" -->

<%
' Se verifica que los datos han sido enviados desde el formulario, para la validación con el SMTP
        
		Set Mail = Server.CreateObject("Persits.MailSender") 
		Mail.Host = "localhost"
		Mail.Username = ""
		Mail.Password = ""
		Mail.AddAddress("ventasgrp@maint-solution.com")
        Mail.From = "ventasgrp@maint-solution.com"
		Mail.FromName = "Global Requests Pro - Contacto Web"
		Mail.Subject = "Su contraseña para" & "http://www."
	    Mail.Body = "Consultante : " & request("codigo") & " // " & request("email")
        Mail.Send
		If Err <> 0 Then
		 Response.Write("<script type='text/javascript'>alert('Ha ocurrido un error');</script>")		
          response.redirect("/register.htm")
       End If 
	    Set Mail = Nothing
        Response.Write("<script type='text/javascript'>alert('Registro exitoso! Nos contactaremos a la brevedad');</script>")
        response.redirect("/register.htm")
 %>