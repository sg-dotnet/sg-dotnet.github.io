(function( factory ) {
	if ( typeof define === "function" && define.amd ) {

		// AMD. Register as an anonymous module.
		define([ "jquery" ], factory );
	} else {

		// Browser globals
		factory( jQuery );
	}
}(function( $ ) {

/*!
 * jQuery UI ShowMore 1.0.0
 * https://github.com/borgboyone/jquery-ui-showmore
 *
 * Copyright 2015 Anthony Wells
 * Released under the MIT license.
 * https://raw.githubusercontent.com/borgboyone/jquery-ui-showmore/master/LICENSE
 *
 * http://borgboyone.github.io/jquery-ui-showmore/
 */

var showmore = $.widget('aw.showmore', {
	options: {
		moreText: "Show More",
		lessText: "Show Less",
		collapsedHeight: 'max-height',
		duration: 200, // consider migrating to animate
		expand: undefined,
		collapse: undefined,
		showIcon: true
	},
	_create: function() {
		this._super();
		// transform collapsedHeight as needed
		if (this.options.collapsedHeight === 'max-height')
			this.options.collapsedHeight = $(this.element).css('max-height');
		this.options.collapsedHeight = this._toPixels(this.options.collapsedHeight, this.element);
		if (isNaN(this.options.collapsedHeight))
			this.options.collapsedHeight = 200;
		var $element = $(this.element),
			curDisplay = $element.css('display'),
			initialHeight = curDisplay !== 'none' ? $element.outerHeight() : this.options.collapsedHeight,
			wrapper = this.wrapper = $element.wrap('<div class="ui-showmore-wrapper" aria-collapsible="true"></div>').parent().uniqueId()
			.append('<div class="ui-showmore-more" style="display: none;" aria-controls="' + $element.parent().prop('id') + '"><div class="extender"><a id="show-mycontent" onclick="false" class="more-btn-home">Show more</a></div></div>')
			.append('<div class="ui-showmore-less" style="display: none;" aria-controls="' + $element.parent().prop('id') + '"><div class="extender"><a id="show-mycontent" onclick="false" class="more-btn-home">Show less</a></div></div>');
		this._on(wrapper.find('.ui-showmore-more > div'), {'click': this._expand});
		this._on(wrapper.find('.ui-showmore-less > div'), {'click': this._collapse});
		// we can now safely clear max-height and/or display none on this.element, but save old values if present in a style tag
		var elementStyles = $element.prop('style');
		this.originalStyles = {};
		this.originalStyles.maxHeight = elementStyles.maxHeight;
		$element.css('max-height', 'none');
		if (curDisplay === 'none') {
			this.originalStyles.display = elementStyles.display;
			$element.css('display', 'block');
		}

		this.isExpanded = undefined;
		this.refresh();
	},
	_destroy: function() {
		// do this without div flicker
		$(this.element).css('max-height', typeof this.originalStyles.maxHeight !== 'undefined' ? this.originalStyles.maxHeight : '');
		if ('display' in this.originalStyles)
			$(this.element).css('display', typeof this.originalStyles.display !== 'undefined' ? this.originalStyles.display : '');
		this.element.siblings().remove().unwrap();
		this._super();
	},
	expand: function() {
		if ((typeof this.isExpanded !== 'undefined') && !this.isExpanded)
			this._expand();
	},
	_expand: function(event) {
        var height = $(this.element).outerHeight() + this.wrapper.find('.ui-showmore-less').outerHeight();
        this.wrapper.animate({height: height}, this.options.duration);
        this.wrapper.attr('aria-expanded', 'true');
        this.isExpanded = true;
        $('.ui-showmore-more').hide();
        $('.ui-showmore-less').show();

		if (event) this._trigger('expand', event, {item: $(this.element), element: this.element, expanded: $(this.element)});
	},
	collapse: function() {
		if ((typeof this.isExpanded !== 'undefined') && this.isExpanded)
			this._collapse();
	},
	_collapse: function(event) {
		var height = this.options.collapsedHeight;
        this.wrapper.animate({height: height}, this.options.duration);
        this.wrapper.attr('aria-expanded', 'false');
        this.isExpanded = false;
        $('.ui-showmore-less').hide();
        $('.ui-showmore-more').show();

		if (event) this._trigger('collapse', event, {item: $(this.element), element: this.element, collapsed: $(this.element)});
	},
	// in case content has changed
	refresh: function() {
		// if not needed, adjust height, hide clickables
		if ($(this.element).outerHeight() <= this.options.collapsedHeight) {
			this.wrapper.find('.ui-showmore-less').hide();
			this.wrapper.find('.ui-showmore-more').hide();
			this.wrapper.removeAttr('aria-expanded');
			this.wrapper.outerHeight($(this.element).outerHeight()); // 'auto' is also acceptable
			this.isExpanded = undefined;
		} else {
			if ((typeof this.isExpanded !== 'undefined') && this.isExpanded) {
				this._expand();
			} else {
				this._collapse();
			}
		}
	},
	isExpanded: function() {
		return this.isExpanded;
	},
	toggle: function() {
		if (typeof this.isExpanded !== 'undefined') {
			this.isExpanded ? this._collapse() : this._expand();
		}
	},
	_toPixels: function(value, context) {
		if (typeof value === 'number') return value;
		if (typeof value !== "string") return undefined;
		if ($.isNumeric(value) || (value.endsWith("px") && $.isNumeric(value.substring(0, value.length - 2)))) {
			value = parseFloat(value);
			if (!isNaN(value) && (value >= 0))
				return value;
		} else if (value.endsWith("em") && $.isNumeric(value.substring(0, value.length - 2))) {
			value = parseFloat(value);
			if (!isNaN(value) && (value >= 0)) {
				return parseFloat($(context).css('line-height')) * value; //emsToPixels(value, context);
			}
		}
		return NaN;
	}
});

}));