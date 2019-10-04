require(['../../wwwroot/js/monaco-editor/min/vs/editor/editor.main'], function () {
    var originalModel = monaco.editor.createModel("zap", "javascript");
    var modifiedModel = monaco.editor.createModel("zap1", "javascript");
    var editor = monaco.editor.createDiffEditor(document.getElementById('zap'), {
        language: 'javascript',
        theme: "vs-dark",
        automaticLayout: true,
        scrollbar: {
            verticalHasArrows: true,
            horizontalHasArrows: true,
            vertical: 'visible',
            horizontal: 'visible',
            verticalScrollbarSize: 17,
            horizontalScrollbarSize: 17,
            arrowSize: 30
        }
    });
    editor.setModel({
        original: originalModel,
        modified: modifiedModel
    });
});