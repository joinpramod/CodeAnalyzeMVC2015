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
    forced_root_block: '',
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