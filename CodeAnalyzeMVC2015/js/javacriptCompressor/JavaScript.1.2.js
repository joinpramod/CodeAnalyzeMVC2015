SyntaxHighlighter.all();SyntaxHighlighter.defaults.toolbar=false;function ValidateAnswer(){var b=tinyMCE.get("SolutionEditor").getContent();b=b.replace(/ class="language-none"/g,"");b=b.replace(/<code>/g,"");b=b.replace(/<\/code>/g,"");b=b.replace(/</g,"&lt;");b=b.replace(/>/g,"&gt;");b=b.replace(/\"/g,"&quot;");b=b.replace(/'/g,"~~");b=b.replace(/\n/g,"#~");tinyMCE.get("SolutionEditor").setContent(b);var a=document.getElementById("hfUserEMail").value;if(a!=""){if(b==""){alert("Please enter details");return false}else{return true}}else{alert("Please login to post");return false}}function ValidateQues(){var d=document.getElementById("txtTitle").value;d=d.replace(/</g,"``");d=d.replace(/&#/g,"~~");document.getElementById("txtTitle").value=d.replace(/'/g,"");var c=$("#ddType").val();var b=tinyMCE.get("EditorAskQuestion").getContent();b=b.replace(/ class="language-none"/g,"");b=b.replace(/<code>/g,"");b=b.replace(/<\/code>/g,"");b=b.replace(/</g,"&lt;");b=b.replace(/>/g,"&gt;");b=b.replace(/\"/g,"&quot;");b=b.replace(/'/g,"~~");b=b.replace(/\n/g,"#~");tinyMCE.get("EditorAskQuestion").setContent(b);var a=document.getElementById("hfUserEMail").value;if(a!=""){if(b==""&&d==""&&c==""){alert("Please enter question title, type and details");return false}else{if(b!=""&&d==""&&c==""){alert("Please enter question title and type");return false}else{if(b==""&&d!=""&&c==""){alert("Please enter question type and details");return false}else{if(b==""&&d==""&&c!=""){alert("Please enter question title and content");return false}else{if(b==""&&d!=""&&c!=""){alert("Please enter question title and content");return false}else{if(b!=""&&d!=""&&c==""){alert("Please enter question title and content");return false}else{if(b!=""&&d==""&&c!=""){alert("Please enter question title and content");return false}else{if(ValidateEMail(a)){return true}else{return false}}}}}}}}}else{alert("Please login to post");return false}}function ValidateComment(){var b=document.getElementById("txtReply").value;var a=document.getElementById("hfUserEMail").value;if(a!=""){if(b==""||b==""){alert("Please enter details");return false}else{if(ValidateEMail(a)){return true}else{return false}}}else{alert("Please login to post");return false}}function ValidatePostArticle(){var a=document.getElementById("hfUserEMail").value;var b=document.getElementById("fileArticleWordFile").value;if(a!=""){if(b==""){alert("Please select your article word file to post");return false}else{if(b.indexOf("docx")<0&&b.indexOf("doc")<0&&b.indexOf("DOCX")<0&&b.indexOf("DOC")<0){alert("Invalid Format, please select only word doc.");return false}else{return true}}}else{alert("Please login to post");return false}}function ValidateUserReg(){var e=document.getElementById("FirstName").value;var b=document.getElementById("LastName").value;var d=document.getElementById("Email").value;var a=document.getElementById("Details").value;var c=document.getElementById("txtPassword").value;var f=document.getElementById("txtConfirmPassword").value;var g=document.getElementById("Address").value;if(e==""||b==""||d==""||a==""||c==""||f==""||g==""){alert("Please enter all details. First and Last names, EMail, Password, Addess and Details");return false}else{if(c!=f){alert("Confirm password not matching with Password");return false}else{if(c.length<8){alert("Password is expected to be 8 charectors.");return false}else{if(ValidateEMail(d)){return true}else{alert("Please enter valid email");return false}}}}}function ValidateSuggestion(){var a=document.getElementById("txtEMail").value;var b=document.getElementById("txtSuggestion").value;if(a==""||b==""){alert("Please enter all details");return false}else{if(ValidateEMail(a)){return true}else{alert("Please enter valid email");return false}}}function ValidateLogin(){var a=document.getElementById("txtEMailId").value;var b=document.getElementById("txtPassword").value;if(a!=""&&b!=""){if(ValidateEMail(a)){return true}else{alert("Please enter valid email");return false}}else{alert("Please enter username and password");return false}}function ValidateEMail(a){var b=/^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$/i;return b.test(a)}function ValidateForgotPassword(){var a=document.getElementById("txtEMailId").value;if(a!=""){if(ValidateEMail(a)){return true}else{return false}}else{alert("Please enter email");return false}}function ValidatePasswords(){var b=document.getElementById("HFOldPassword").value;var c=document.getElementById("txtPassword").value;var a=document.getElementById("txtNewPassword").value;var e=document.getElementById("txtConfirmPassword").value;var d=document.getElementById("hfUserEMail").value;if(d!=""){if(b!=c){alert("Please enter correct Old Password");return false}else{if(a!=e){alert("Password and ConfirmPassword does not match");return false}else{if(a.length<8){alert("Password is expected to be 8 charectors.");return false}else{return true}}}}else{alert("Please login to post");return false}}function ValidateRefer(){var a=document.getElementById("txtReferEMail").value;if(a!=""){if(ValidateEMail(a)){return true}else{return false}}else{alert("Please enter email");return false}}function PostVotes(c,b,e){var a=document.getElementById("hiddenQuesTitle").value;var f=document.createElement("form");f.setAttribute("method","post");f.setAttribute("action","/Que/Ans/"+c+"/"+a);var d=document.createElement("input");d.setAttribute("type","hidden");d.setAttribute("name",c);d.setAttribute("value",c);var h=document.createElement("input");h.setAttribute("type","hidden");h.setAttribute("name",b);h.setAttribute("value",b);var g=document.createElement("input");g.setAttribute("type","hidden");g.setAttribute("name",e);g.setAttribute("value",e);f.appendChild(d);f.appendChild(h);f.appendChild(g);document.body.appendChild(f);f.submit()}function DeletePost(b){var a=document.getElementById("hiddenQuesTitle").value;var d=document.createElement("form");d.setAttribute("method","post");d.setAttribute("action","/Que/Ans/"+b+"/"+a);var c=document.createElement("input");c.setAttribute("type","hidden");c.setAttribute("name",b);c.setAttribute("value",b);var e=document.createElement("input");e.setAttribute("type","hidden");e.setAttribute("name","DeletePost");e.setAttribute("value","DeletePost");d.appendChild(c);d.appendChild(e);document.body.appendChild(d);d.submit()}function PostArticleVotes(c){var b=document.getElementById("hfArticleId").value;var a=document.getElementById("hfArticleTitle").value;var d=document.createElement("form");d.setAttribute("method","post");d.setAttribute("action","/Articles/Details/"+b+"/"+a);var e=document.createElement("input");e.setAttribute("type","hidden");e.setAttribute("name",c);e.setAttribute("value",c);d.appendChild(e);document.body.appendChild(d);d.submit()}$(window).scroll(function(){var f=$(document).height();var c=$(document).width();var e=$("#ccr-right-section").height();var g=$(document).scrollTop();var b=$("#ccr-header").height();var a=$("#ccr-footer-sidebar").height();var d=$("#ccr-nav-main").height();if(c>1000){if((g>b+10)&&(g<(f-(e+a+150)))){$("#ccr-right-section").css("margin-top",g-b+d+10+"px")}if($(window).scrollTop()<210){$("#ccr-right-section").css("margin-top",10+"px")}}if(g>(b+10)){$("#ccr-nav-main").css("position","fixed");$("#ccr-nav-main").css("top",0+"px");$("#menuSiteTitle").css("display","inline");$("#menuLogo").css("display","inline");$("#menuRegister").css("display","inline")}if($(window).scrollTop()<200){$("#ccr-nav-main").css("top","auto");$("#ccr-nav-main").css("position","relative");$("#menuSiteTitle").css("display","none");$("#menuLogo").css("display","none");$("#menuRegister").css("display","none")}});