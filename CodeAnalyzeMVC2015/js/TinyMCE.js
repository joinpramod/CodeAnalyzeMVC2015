tinymce.init(
    {
        mode: "textareas",
        plugins: ["paste", "autolink", "codesample", "link"],
        menubar: false,
        toolbar1: "bold | numlist | undo redo | link | codesample",
        codesample_languages: [
        { text: 'General', value: 'none' },
        ],
        statusbar: false,
        keep_styles: false,
        height: "300px",
        width: "98%",
        encoding: "xml",
        force_br_newlines: true,
        force_p_newlines: false,
        forced_root_block: "",
        paste_auto_cleanup_on_paste: true,
        paste_remove_styles: true,
        paste_as_text: true,
        paste_remove_spans: true,
        paste_remove_styles_if_webkit: true,
        paste_strip_class_attributes: "all",
        setup: function (a)
        {
            a.on("init", function ()
            {
                this.getDoc().body.style.fontSize = "13px";
                this.getDoc().body.pasteAsPlainText = true;
                tinyMCE.activeEditor.setContent("")
            });
            a.pasteAsPlainText = true;
            a.on("keyup", function (c)
            {
                var b = StringClean(tinyMCE.activeEditor.getContent());
                document.getElementById("divPreview").innerHTML = b; prettyPrint()
            });
            a.on("keydown", function (b)
            {
                if (b.keyCode == 9)
                {
                    if (b.shiftKey)
                    {
                        a.execCommand("Outdent")
                    }
                    else
                    {
                        a.execCommand("Indent")
                    }
                    b.preventDefault();
                    return false
                }
            })
        }
    });

tinyMCE.activeEditor.setContent("");

function StringClean(a)
{
    var b = a;
    b = b.replace(/'/g, "");
    b = b.replace('pre class="language-none"', 'pre class="prettyprint" style="font-size:14px;"');
    b = b.replace('pre class="language-markup"', 'pre class="prettyprint" style="font-size:14px;"');
    b = b.replace('pre class="language-javascript"', 'pre class="prettyprint" style="font-size:14px;"');
    b = b.replace('pre class="language-css"', 'pre class="prettyprint" style="font-size:14px;"');
    b = b.replace('pre class="language-php"', 'pre class="prettyprint" style="font-size:14px;"');
    b = b.replace('pre class="language-runy"', 'pre class="prettyprint" style="font-size:14px;"');
    b = b.replace('pre class="language-python"', 'pre class="prettyprint" style="font-size:14px;"');
    b = b.replace('pre class="language-java"', 'pre class="prettyprint" style="font-size:14px;"');
    b = b.replace('pre class="language-c"', 'pre class="prettyprint" style="font-size:14px;"');
    b = b.replace('pre class="language-cpp"', 'pre class="prettyprint" style="font-size:14px;"');
    b = b.replace('pre class="language-csharp"', 'pre class="prettyprint" style="font-size:14px;"');
    b = b.replace(/<br>\r\n/g, "\r\n");
    b = b.replace(/\r\n/g, "#####");
    b = b.replace(/<br>/g, "<br />");
    b = b.replace(/<\/pre><br>/g, "</pre>");
    b = b.replace(/<pre>/g, '<pre class="prettyprint" style="font-size:14px;">');
    b = b.replace(/<pre class=\"prettyprint\" style=\"font-size:14px;\"><br \/>/g, '<pre class="prettyprint" style="font-size:14px;">');
    b = b.replace(/<pre class=\"prettyprint\" style=\"font-size:14px;\"><br\/>/g, '<pre class="prettyprint" style="font-size:14px;">');
    b = b.replace(/#####/g, "\r\n");
    b = b.replace(/<\/pre><br \/>/g, "</pre>");
    b = b.replace(/<\/pre><br\/>/g, "</pre>");
    return b
};
