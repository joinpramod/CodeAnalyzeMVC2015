tinymce.init({
    mode: "textareas",
    plugins: "paste",
    menubar: false,
    toolbar: 'mybutton',
    statusbar: false,
    keep_styles: false,
    height: "300px",
    width: "98%",
    encoding: 'xml',
    force_br_newlines: true,
    force_p_newlines: false,
    forced_root_block: '',
    paste_auto_cleanup_on_paste: true,
    paste_remove_styles: true,
    paste_as_text: true,
    paste_remove_spans: true,
    paste_remove_styles_if_webkit: true,
    paste_strip_class_attributes: "all",
    setup: function (ed) {
        ed.on('init', function () {
            this.getDoc().body.style.fontSize = '13px';
            this.getDoc().body.pasteAsPlainText = true;
            tinyMCE.activeEditor.setContent('');
        });
        ed.pasteAsPlainText = true;
        ed.addButton('mybutton', {
            text: 'INSERT CODE',
            onclick: function () {
                tinymce.activeEditor.execCommand('mceInsertContent', false, "#codestart<br />&nbsp;&nbsp;&nbsp;&nbsp; Type your code here <br />#codeend");
            }
        });
    }
});

tinyMCE.activeEditor.setContent('');