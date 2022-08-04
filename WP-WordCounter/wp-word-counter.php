<?php

/*
Plugin Name: Wp Plugin
Plugin URI: http://hamidmolareza.ir/plugin
Description: A brief description of the Plugin.
Version: 1.0
Author: Hamid Molareza
Author URI: http://hamidmolareza.ir
License: GPLv3
*/

class WordCountAndTimePlugin {
	public string $pageUrl = 'word-count-settings-page';

	function __construct() {
		add_action( "admin_menu", array( $this, "adminPage" ) );
		add_action( "admin_init", array( $this, 'settings' ) );
		add_filter( 'the_content', array( $this, 'ifWrap' ) );
	}

	function ifWrap( $content ) {
		if ( is_main_query() and is_single() and
		                         ( get_option( 'wcp_wordCount', 1 ) or
		                           get_option( 'wcp_characterCount', 1 ) or
		                           get_option( 'wcp_readTime', 1 ) ) ) {
			return $this->createHtml( $content );
		}

		return $content;
	}

	function createHtml( $content ): string {
		$html = '<h3>' . esc_html(get_option( 'wcp_headline', 'Post Statistics' )) . '</h3><p>';

		if ( get_option( 'wcp_wordCount', 1 ) or get_option( 'wcp_readTime', 1 ) ) {
			$wordCount = str_word_count( strip_tags( $content ) );
		}

		if ( get_option( 'wcp_wordCount', 1 ) ) {
			$html .= "This post has " . $wordCount . " words<br>";
		}
		if ( get_option( 'wcp_characterCount', 1 ) ) {
			$html .= "This post has " . strlen( strip_tags( $content ) ) . " characters<br>";
		}
		if ( get_option( 'wcp_readTime', 1 ) ) {
			$html .= "This post take about " . round( $wordCount / 225 ) . " minute(s) to read.<br>";
		}

		$html .= "</p>";

		if ( get_option( 'wcp_location', '0' ) == '0' ) {
			return $html . $content;
		}

		return $content . $html;
	}

	function settings() {
		add_settings_section( 'wcp_first_section', null, null, $this->pageUrl );

		add_settings_field( 'wcp_location', 'Display Location', array(
			$this,
			'locationHtml'
		), $this->pageUrl, 'wcp_first_section' );
		register_setting( 'wordCountPlugin', 'wcp_location', array(
			'sanitize_callback' => array( $this, 'sanitizeLocation' ),
			'default'           => '0'
		) );

		add_settings_field( 'wcp_headline', 'Headline Text', array(
			$this,
			'headlineHtml'
		), $this->pageUrl, 'wcp_first_section' );
		register_setting( 'wordCountPlugin', 'wcp_headline', array(
			'sanitize_callback' => 'sanitize_text_field',
			'default'           => 'Post Statistics'
		) );

		add_settings_field( 'wcp_wordCount', 'Word Count', array(
			$this,
			'checkboxHtml'
		), $this->pageUrl, 'wcp_first_section', array( 'name' => 'wcp_wordCount' ) );
		register_setting( 'wordCountPlugin', 'wcp_wordCount', array(
			'sanitize_callback' => 'sanitize_text_field',
			'default'           => '1'
		) );

		add_settings_field( 'wcp_characterCount', 'Character Count', array(
			$this,
			'checkboxHtml'
		), $this->pageUrl, 'wcp_first_section', array( 'name' => 'wcp_characterCount' ) );
		register_setting( 'wordCountPlugin', 'wcp_characterCount', array(
			'sanitize_callback' => 'sanitize_text_field',
			'default'           => '1'
		) );

		add_settings_field( 'wcp_readTime', 'Read Time', array(
			$this,
			'checkboxHtml'
		), $this->pageUrl, 'wcp_first_section', array( 'name' => 'wcp_readTime' ) );
		register_setting( 'wordCountPlugin', 'wcp_readTime', array(
			'sanitize_callback' => 'sanitize_text_field',
			'default'           => '1'
		) );
	}

	function sanitizeLocation( $input ) {
		if ( $input != '0' and $input != '1' ) {
			add_settings_error( 'wcp_location', 'wcp_location_error', 'Display location must be begin or end.' );

			return get_option( 'wcp_location' );
		}

		return $input;
	}

	function checkboxHtml( $args ) { ?>
        <input type="checkbox" name="<?php echo $args['name'] ?>"
               value="1" <?php checked( get_option( $args['name'] ), '1' ) ?>>
	<?php }

	function headlineHtml() { ?>
        <input type="text" name="wcp_headline" value="<?php echo esc_attr( get_option( 'wcp_headline' ) ) ?>">
	<?php }

	function locationHtml() { ?>
        <select name="wcp_location">
            <option value="0" <?php selected( get_option( "wcp_location" ), 0 ) ?> >Beginning of post</option>
            <option value="1" <?php selected( get_option( "wcp_location" ), 1 ) ?>>End of post</option>
        </select>
	<?php }

	function adminPage() {
		add_options_page( "Word Count Settings", "Word Count", "manage_options", $this->pageUrl, array(
			$this,
			"ourHtml"
		) );
	}

	function ourHtml() { ?>
        <div class="wrap">
            <h1>Word Count Settings</h1>
            <form action="options.php" method="post">
				<?php
				settings_fields( 'wordCountPlugin' );
				do_settings_sections( $this->pageUrl );
				submit_button();
				?>
            </form>
        </div>
	<?php }
}

$wordCountAndTimePlugin = new WordCountAndTimePlugin();