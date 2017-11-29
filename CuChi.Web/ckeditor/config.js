

CKEDITOR.editorConfig = function (config) {

    //Default
    config.extraPlugins = 'youtube';
    config.extraPlugins = 'lineutils';
    config.extraPlugins = 'widget';
	
    //video
    config.extraPlugins = 'oembed,fontawesome,lineheight,pastefromword,bt_table,tableresize,bootstrapTabs,widgetcommon,widgetbootstrap,widgettemplatemenu';
    config.oembed_maxWidth = '560';
    config.oembed_maxHeight = '315';

    //fontawesome    
    config.contentsCss = ['/Admin/Components/App/css/font-awesome.min.css','https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css'];
	//config.contentsCss = 'https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css';
	//config.stylesCombo_stylesSet= 'my_styles:/theme/Choice/styles.js',

    config.allowedContent = true;
	
	config.pasteFromWordRemoveFontStyles = false;
config.extraAllowedContent = 'a(*){*}[*];'+'li(*){*}[*]';
};

// allow i tags to be empty (for font awesome)
CKEDITOR.dtd.$removeEmpty['span'] = false;
CKEDITOR.dtd.$removeEmpty['i'] = false
