SyntaxHighlighter.all();
SyntaxHighlighter.defaults.toolbar = false;


        tinymce.init({
            mode: "textareas",
            plugins: "paste",
            menubar: false,
            toolbar: false,
            statusbar: false,
            keep_styles: false,
            height: "300px",
            width: "95%",
            encoding: 'xml',
            force_br_newlines: true,
            force_p_newlines: false,
            forced_root_block : '',
            paste_auto_cleanup_on_paste: true,
            paste_remove_styles: true,
            paste_remove_styles_if_webkit: true,
            paste_strip_class_attributes: "all",
            setup: function (ed) {
                ed.on('init', function () {
                    this.getDoc().body.style.fontSize = '13px';
                });
            }
        });

        function ValidateAnswer() {
            var content = tinyMCE.get('SolutionEditor').getContent()
            var VarEMail = document.getElementById('hfUserEMail').value;

            if (VarEMail != "") {
                if (content == "") {
                    alert("Please enter details");
                    return false;
                }
                else {
                    return true;
                }
            }
            else {
                alert("Please login to post");
                return false;
            }
        }

        function ValidateQues() {
            debugger;
            var title = document.getElementById('txtTitle').value;
            var ddType = $("#ddType").val();
            var content = tinyMCE.get('EditorAskQuestion').getContent()
            var VarEMail = document.getElementById('hfUserEMail').value;

            if (VarEMail != "") {
                if (content == "" && title == "" && type == "") {
                    alert("Please enter question title, type and details");
                    return false;
                }
                else if (content != "" && title == "" && type == "") {
                    alert("Please enter question title and type");
                    return false;
                }
                else if (content == "" && title != "" && type == "") {
                    alert("Please enter question type and details");
                    return false;
                }
                else if (content == "" && title == "" && type != "") {
                    alert("Please enter question title and content");
                    return false;
                }
                else if (content == "" && title != "" && type != "") {
                    alert("Please enter question title and content");
                    return false;
                }
                else if (content != "" && title != "" && type == "") {
                    alert("Please enter question title and content");
                    return false;
                }
                else if (content != "" && title == "" && type != "") {
                    alert("Please enter question title and content");
                    return false;
                }
                else
                    return true;
            }
            else {
                alert("Please login to post");
                return false;
            }
        }

        function ValidateComment() {
                var content = document.getElementById('txtReply').value;
                var VarEMail = document.getElementById('hfUserEMail').value;
                if (VarEMail != "") {
                    if (content == "" || content == "") {
                        alert("Please enter details");
                        return false;
                    }
                    else
                        return true;
                }
                else {
                    alert("Please login to post");
                    return false;
                }
        }

        function ValidatePostArticle() {
            var VarEMail = document.getElementById('hfUserEMail').value;
            var varArticleWordFile = document.getElementById('fileArticleWordFile').value;


            if (VarEMail != "") {
                if (varArticleWordFile == "") {
                    alert("Please select your article word file to post");
                    return false;
                }
                else {
                    if (varArticleWordFile.indexOf("docx") < 0 && varArticleWordFile.indexOf("doc") < 0 && varArticleWordFile.indexOf("DOCX") < 0 && varArticleWordFile.indexOf("DOC") < 0) {
                        alert('Invalid Format, please select only word doc.');
                        return false;
                    }
                    else {
                        return true;
                    }
                }

            }
            else {
                alert("Please login to post");
                return false;
            }
        }

        function ValidateUserReg() {
            var varFirstName = document.getElementById('FirstName').value;
            var varLastName = document.getElementById('LastName').value;
            var varEMail = document.getElementById('EMail').value;
            var varDetails = document.getElementById('Details').value;
            var varPassword = document.getElementById('txtPassword').value;
            var varConfirmPassword = document.getElementById('txtConfirmPassword').value;
            var varAddress = document.getElementById('Address').value;
            var VarEMail = document.getElementById('hfUserEMail').value;

            if (VarEMail == "") {
                if (varFirstName == null && varLastName == null && varEMail == "" && varDetails == null && varPassword == null && varConfirmPassword == "" && varAddress == "") {
                    alert("Please enter First and Last names, EMail, Password, Addess and Details");
                    return false;
                }
                else if (varFirstName == null && varLastName == null && varEMail == "" && varDetails == null && varPassword == null && varConfirmPassword == "") {
                    alert("Please enter First and Last names, EMail, Password and Details");
                    return false;
                }
                else if (varFirstName == null && varLastName == null && varEMail == "" && varPassword == null && varConfirmPassword == "") {
                    alert("Please enter First and Last names, EMail, Password");
                    return false;
                }
                else if (varFirstName == null && varLastName == null && varPassword == null && varConfirmPassword == "") {
                    alert("Please enter First and Last names, EMail");
                    return false;
                }
                else if (varFirstName == null && varPassword == null && varConfirmPassword == "") {
                    alert("Please enter First and EMail"); return false;
                    return false;
                }
                else if (varPassword != varConfirmPassword) {
                    alert("Confirm password not matching with Password");
                    return false;
                }
                else if (varPasswor.length < 8) {
                    alert("Password is expected to be 8 charectors.");
                    return false;
                }
                else
                    return true;
            }       
            else
                return false;
        }
        

