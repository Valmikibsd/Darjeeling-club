/**
 * @license Copyright (c) 2003-2014, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';
    config.font_names = 'Hindi/Kruti;' + config.font_names;

    config.allowedContent = true;
    config.protectedSource.push(/<i[^>]*><\/i>/g);
    config.enterMode = CKEDITOR.ENTER_BR;
 };
CKEDITOR.dtd.$removeEmpty['i'] = false;