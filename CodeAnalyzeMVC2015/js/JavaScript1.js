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