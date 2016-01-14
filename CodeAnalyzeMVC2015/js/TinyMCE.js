tinymce.init({
    mode: "textareas",
    plugins: [
        'paste',
        'autolink',
    ],
    menubar: false,
    toolbar: 'mybutton | undo redo | bold italic | bullist numlist outdent indent',
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
                tinymce.activeEditor.execCommand('mceInsertContent', false, '#codestart<br /><br />');
                var bm = tinyMCE.activeEditor.selection.getBookmark();
                tinymce.activeEditor.execCommand('mceInsertContent', false, '<br />#codeend');
                tinyMCE.activeEditor.selection.moveToBookmark(bm);
            }
        });
    }
});

tinyMCE.activeEditor.setContent('');