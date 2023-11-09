(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? factory(exports) :
        typeof define === 'function' && define.amd ? define('DASFrontend', ['exports'], factory) :
            (factory((global.DASFrontend = {})));
}(this, (function (exports) {
    'use strict';

    function CookieBanner(module) {
        this.module = module;
        this.settings = {
            seenCookieName: 'DASSeenCookieMessage',
            env: window.GOVUK.getEnv(),
            cookiePolicy: {
                AnalyticsConsent: false
            }
        };
        if (!window.GOVUK.cookie(this.settings.seenCookieName + this.settings.env)) {
            this.start();
        }
    }

    CookieBanner.prototype.start = function () {
        this.module.cookieBanner = this.module.querySelector('.das-cookie-banner');
        this.module.cookieBannerInnerWrap = this.module.querySelector('.das-cookie-banner__wrapper');
        this.module.cookieBannerConfirmationMessage = this.module.querySelector('.das-cookie-banner__confirmation');
        this.setupCookieMessage();
    };

    CookieBanner.prototype.setupCookieMessage = function () {
        this.module.hideLink = this.module.querySelector('button[data-hide-cookie-banner]');
        this.module.acceptCookiesButton = this.module.querySelector('button[data-accept-cookies]');

        if (this.module.hideLink) {
            this.module.hideLink.addEventListener('click', this.hideCookieBanner.bind(this));
        }

        if (this.module.acceptCookiesButton) {
            this.module.acceptCookiesButton.addEventListener('click', this.acceptAllCookies.bind(this));
        }
        this.showCookieBanner();
    };

    CookieBanner.prototype.showCookieBanner = function () {
        var cookiePolicy = this.settings.cookiePolicy,
            that = this;
        this.module.cookieBanner.style.display = 'block';

        // Create the default cookies based on settings
        Object.keys(cookiePolicy).forEach(function (cookieName) {
            window.GOVUK.cookie(cookieName + that.settings.env, cookiePolicy[cookieName].toString(), { days: 365 });
        });

    };

    CookieBanner.prototype.hideCookieBanner = function () {
        this.module.cookieBanner.style.display = 'none';
        window.GOVUK.cookie(this.settings.seenCookieName + this.settings.env, true, { days: 365 });
    };

    CookieBanner.prototype.acceptAllCookies = function () {
        var that = this;
        this.module.cookieBannerInnerWrap.style.display = 'none';
        this.module.cookieBannerConfirmationMessage.style.display = 'block';

        window.GOVUK.cookie(this.settings.seenCookieName + this.settings.env, true, { days: 365 });

        Object.keys(this.settings.cookiePolicy).forEach(function (cookieName) {
            window.GOVUK.cookie(cookieName + that.settings.env, true, { days: 365 });
        });
    };

    function CookieSettings(module, options) {
        this.module = module;
        this.settings = {
            seenCookieName: 'DASSeenCookieMessage',
            env: window.GOVUK.getEnv(),
            cookiePolicy: {
                AnalyticsConsent: false
            }
        };

        var cookieBanner = document.querySelector('.das-cookie-banner');
        cookieBanner.style.display = 'none';

        this.start();
    }

    CookieSettings.prototype.start = function () {
        this.setRadioValues();
        this.module.addEventListener('submit', this.formSubmitted.bind(this));
    };

    CookieSettings.prototype.setRadioValues = function () {
        var cookiePolicy = this.settings.cookiePolicy,
            that = this;
        Object.keys(cookiePolicy).forEach(function (cookieName) {
            var existingCookie = window.GOVUK.cookie(cookieName + that.settings.env),
                radioButtonValue = existingCookie !== null ? existingCookie : cookiePolicy[cookieName],
                radioButton = document.querySelector('input[name=cookies-' + cookieName + '][value=' + (radioButtonValue === 'true' ? 'on' : 'off') + ']');
            radioButton.checked = true;
        });
    };

    CookieSettings.prototype.formSubmitted = function (event) {

        event.preventDefault();

        var formInputs = event.target.getElementsByTagName("input"),
            button = event.target.getElementsByTagName("button"),
            that = this;

        for (var i = 0; i < formInputs.length; i++) {
            var input = formInputs[i];
            if (input.checked) {
                var name = input.name.replace('cookies-', '');
                var value = input.value === "on";
                window.GOVUK.setCookie(name + that.settings.env, value, { days: 365 });
            }
        }

        window.GOVUK.setCookie(this.settings.seenCookieName + that.settings.env, true, { days: 365 });

        if (button.length > 0) {
            button[0].removeAttribute('disabled');
        }

        this.showConfirmationMessage();

    };

    CookieSettings.prototype.showConfirmationMessage = function () {
        var confirmationMessage = document.querySelector('div[data-cookie-confirmation]');
        var previousPageLink = document.querySelector('.cookie-settings__prev-page');
        var referrer = document.referrer;

        document.body.scrollTop = document.documentElement.scrollTop = 0;

        if (referrer && referrer !== document.location.href) {
            previousPageLink.href = referrer;
            previousPageLink.style.display = "inline-block";
        } else {
            previousPageLink.style.display = "none";
        }

        confirmationMessage.style.display = "block";
    };

    function Radios($module) {
        this.$module = $module;
    }

    function nodeListForEach(nodes, callback) {
        if (window.NodeList.prototype.forEach) {
            return nodes.forEach(callback)
        }
        for (var i = 0; i < nodes.length; i++) {
            callback.call(window, nodes[i], i, nodes);
        }
    }

    Radios.prototype.init = function () {
        var $module = this.$module;
        var $inputs = $module.querySelectorAll('input[type="radio"]');

        /**
         * Loop over all items with [data-controls]
         * Check if they have a matching conditional reveal
         * If they do, assign attributes.
         **/
        nodeListForEach($inputs, function ($input) {
            var controls = $input.getAttribute('data-aria-controls');

            // Check if input controls anything
            // Check if content exists, before setting attributes.
            if (!controls || !$module.querySelector('#' + controls)) {
                return
            }

            // If we have content that is controlled, set attributes.
            $input.setAttribute('aria-controls', controls);
            $input.removeAttribute('data-aria-controls');
            this.setAttributes($input);
        }.bind(this));

        // Handle events
        $module.addEventListener('click', this.handleClick.bind(this));
    };

    Radios.prototype.setAttributes = function ($input) {
        var $content = document.querySelector('#' + $input.getAttribute('aria-controls'));

        if ($content && $content.classList.contains('govuk-radios__conditional')) {
            var inputIsChecked = $input.checked;

            $input.setAttribute('aria-expanded', inputIsChecked);

            $content.classList.toggle('govuk-radios__conditional--hidden', !inputIsChecked);
        }
    };

    Radios.prototype.handleClick = function (event) {
        var $clickedInput = event.target;
        // We only want to handle clicks for radio inputs
        if ($clickedInput.type !== 'radio') {
            return
        }
        // Because checking one radio can uncheck a radio in another $module,
        // we need to call set attributes on all radios in the same form, or document if they're not in a form.
        //
        // We also only want radios which have aria-controls, as they support conditional reveals.
        var $allInputs = document.querySelectorAll('input[type="radio"][aria-controls]');
        nodeListForEach($allInputs, function ($input) {
            // Only inputs with the same form owner should change.
            var hasSameFormOwner = ($input.form === $clickedInput.form);

            // In radios, only radios with the same name will affect each other.
            var hasSameName = ($input.name === $clickedInput.name);
            if (hasSameName && hasSameFormOwner) {
                this.setAttributes($input);
            }
        }.bind(this));
    };

    function Tabs($module) {
        this.$module = $module;
        this.$tabs = $module.querySelectorAll('.govuk-tabs__tab');

        this.keys = { left: 37, right: 39, up: 38, down: 40 };
        this.jsHiddenClass = 'govuk-tabs__panel--hidden';
    }

    function nodeListForEach$1(nodes, callback) {
        if (window.NodeList.prototype.forEach) {
            return nodes.forEach(callback)
        }
        for (var i = 0; i < nodes.length; i++) {
            callback.call(window, nodes[i], i, nodes);
        }
    }

    Tabs.prototype.init = function () {
        if (typeof window.matchMedia === 'function') {
            this.setupResponsiveChecks();
        } else {
            this.setup();
        }
    };

    Tabs.prototype.setupResponsiveChecks = function () {
        this.mql = window.matchMedia('(min-width: 40.0625em)');
        this.mql.addListener(this.checkMode.bind(this));
        this.checkMode();
    };

    Tabs.prototype.checkMode = function () {
        if (this.mql.matches) {
            this.setup();
        } else {
            this.teardown();
        }
    };

    Tabs.prototype.setup = function () {
        var $module = this.$module;
        var $tabs = this.$tabs;
        var $tabList = $module.querySelector('.govuk-tabs__list');
        var $tabListItems = $module.querySelectorAll('.govuk-tabs__list-item');

        if (!$tabs || !$tabList || !$tabListItems) {
            return
        }

        $tabList.setAttribute('role', 'tablist');

        nodeListForEach$1($tabListItems, function ($item) {
            $item.setAttribute('role', 'presentation');
        });

        nodeListForEach$1($tabs, function ($tab) {
            // Set HTML attributes
            this.setAttributes($tab);

            // Save bounded functions to use when removing event listeners during teardown
            $tab.boundTabClick = this.onTabClick.bind(this);
            $tab.boundTabKeydown = this.onTabKeydown.bind(this);

            // Handle events
            $tab.addEventListener('click', $tab.boundTabClick, true);
            $tab.addEventListener('keydown', $tab.boundTabKeydown, true);

            // Remove old active panels
            this.hideTab($tab);
        }.bind(this));

        // Show either the active tab according to the URL's hash or the first tab
        var $activeTab = this.getTab(window.location.hash) || this.$tabs[0];
        this.showTab($activeTab);

        // Handle hashchange events
        $module.boundOnHashChange = this.onHashChange.bind(this);
        window.addEventListener('hashchange', $module.boundOnHashChange, true);
    };

    Tabs.prototype.teardown = function () {
        var $module = this.$module;
        var $tabs = this.$tabs;
        var $tabList = $module.querySelector('.govuk-tabs__list');
        var $tabListItems = $module.querySelectorAll('.govuk-tabs__list-item');

        if (!$tabs || !$tabList || !$tabListItems) {
            return
        }

        $tabList.removeAttribute('role');

        nodeListForEach$1($tabListItems, function ($item) {
            $item.removeAttribute('role', 'presentation');
        });

        nodeListForEach$1($tabs, function ($tab) {
            // Remove events
            $tab.removeEventListener('click', $tab.boundTabClick, true);
            $tab.removeEventListener('keydown', $tab.boundTabKeydown, true);

            // Unset HTML attributes
            this.unsetAttributes($tab);
        }.bind(this));

        // Remove hashchange event handler
        window.removeEventListener('hashchange', $module.boundOnHashChange, true);
    };

    Tabs.prototype.onHashChange = function (e) {
        var hash = window.location.hash;
        var $tabWithHash = this.getTab(hash);
        if (!$tabWithHash) {
            return
        }

        // Prevent changing the hash
        if (this.changingHash) {
            this.changingHash = false;
            return
        }

        // Show either the active tab according to the URL's hash or the first tab
        var $previousTab = this.getCurrentTab();

        this.hideTab($previousTab);
        this.showTab($tabWithHash);
        $tabWithHash.focus();
    };

    Tabs.prototype.hideTab = function ($tab) {
        this.unhighlightTab($tab);
        this.hidePanel($tab);
    };

    Tabs.prototype.showTab = function ($tab) {
        this.highlightTab($tab);
        this.showPanel($tab);
    };

    Tabs.prototype.getTab = function (hash) {
        return this.$module.querySelector('.govuk-tabs__tab[href="' + hash + '"]')
    };

    Tabs.prototype.setAttributes = function ($tab) {
        // set tab attributes
        var panelId = this.getHref($tab).slice(1);
        $tab.setAttribute('id', 'tab_' + panelId);
        $tab.setAttribute('role', 'tab');
        $tab.setAttribute('aria-controls', panelId);
        $tab.setAttribute('aria-selected', 'false');
        $tab.setAttribute('tabindex', '-1');

        // set panel attributes
        var $panel = this.getPanel($tab);
        $panel.setAttribute('role', 'tabpanel');
        $panel.setAttribute('aria-labelledby', $tab.id);
        $panel.classList.add(this.jsHiddenClass);
    };

    Tabs.prototype.unsetAttributes = function ($tab) {
        // unset tab attributes
        $tab.removeAttribute('id');
        $tab.removeAttribute('role');
        $tab.removeAttribute('aria-controls');
        $tab.removeAttribute('aria-selected');
        $tab.removeAttribute('tabindex');

        // unset panel attributes
        var $panel = this.getPanel($tab);
        $panel.removeAttribute('role');
        $panel.removeAttribute('aria-labelledby');
        $panel.classList.remove(this.jsHiddenClass);
    };

    Tabs.prototype.onTabClick = function (e) {
        if (!e.target.classList.contains('govuk-tabs__tab')) {
            // Allow events on child DOM elements to bubble up to tab parent
            return false
        }
        e.preventDefault();
        var $newTab = e.target;
        var $currentTab = this.getCurrentTab();
        this.hideTab($currentTab);
        this.showTab($newTab);
        this.createHistoryEntry($newTab);
    };

    Tabs.prototype.createHistoryEntry = function ($tab) {
        var $panel = this.getPanel($tab);

        // Save and restore the id
        // so the page doesn't jump when a user clicks a tab (which changes the hash)
        var id = $panel.id;
        $panel.id = '';
        this.changingHash = true;
        window.location.hash = this.getHref($tab).slice(1);
        $panel.id = id;
    };

    Tabs.prototype.onTabKeydown = function (e) {
        switch (e.keyCode) {
            case this.keys.left:
            case this.keys.up:
                this.activatePreviousTab();
                e.preventDefault();
                break
            case this.keys.right:
            case this.keys.down:
                this.activateNextTab();
                e.preventDefault();
                break
        }
    };

    Tabs.prototype.activateNextTab = function () {
        var currentTab = this.getCurrentTab();
        var nextTabListItem = currentTab.parentNode.nextElementSibling;
        if (nextTabListItem) {
            var nextTab = nextTabListItem.querySelector('.govuk-tabs__tab');
        }
        if (nextTab) {
            this.hideTab(currentTab);
            this.showTab(nextTab);
            nextTab.focus();
            this.createHistoryEntry(nextTab);
        }
    };

    Tabs.prototype.activatePreviousTab = function () {
        var currentTab = this.getCurrentTab();
        var previousTabListItem = currentTab.parentNode.previousElementSibling;
        if (previousTabListItem) {
            var previousTab = previousTabListItem.querySelector('.govuk-tabs__tab');
        }
        if (previousTab) {
            this.hideTab(currentTab);
            this.showTab(previousTab);
            previousTab.focus();
            this.createHistoryEntry(previousTab);
        }
    };

    Tabs.prototype.getPanel = function ($tab) {
        var $panel = this.$module.querySelector(this.getHref($tab));
        return $panel
    };

    Tabs.prototype.showPanel = function ($tab) {
        var $panel = this.getPanel($tab);
        $panel.classList.remove(this.jsHiddenClass);
    };

    Tabs.prototype.hidePanel = function (tab) {
        var $panel = this.getPanel(tab);
        $panel.classList.add(this.jsHiddenClass);
    };

    Tabs.prototype.unhighlightTab = function ($tab) {
        $tab.setAttribute('aria-selected', 'false');
        $tab.parentNode.classList.remove('govuk-tabs__list-item--selected');
        $tab.setAttribute('tabindex', '-1');
    };

    Tabs.prototype.highlightTab = function ($tab) {
        $tab.setAttribute('aria-selected', 'true');
        $tab.parentNode.classList.add('govuk-tabs__list-item--selected');
        $tab.setAttribute('tabindex', '0');
    };

    Tabs.prototype.getCurrentTab = function () {
        return this.$module.querySelector('.govuk-tabs__list-item--selected .govuk-tabs__tab')
    };

    // this is because IE doesn't always return the actual value but a relative full path
    // should be a utility function most prob
    // http://labs.thesedays.com/blog/2010/01/08/getting-the-href-value-with-jquery-in-ie/
    Tabs.prototype.getHref = function ($tab) {
        var href = $tab.getAttribute('href');
        var hash = href.slice(href.indexOf('#'), href.length);
        return hash
    };

    function Showhide(module) {
        this.module = module;
        this.buttons = module.querySelectorAll('.das-show-hide__button');
        this.showLinks = module.querySelectorAll('.das-show-hide__show-link');
        this.sectionExpandedClass = 'das-show-hide__section--show';
    }

    Showhide.prototype.init = function () {
        var buttons = this.buttons;
        var showLinks = this.showLinks;
        var that = this;

        nodeListForEach$2(buttons, function (button) {
            var controls = button.getAttribute('data-aria-controls');
            var section = document.getElementById(controls);
            var sectionExpanded = that.isExpanded(section);
            if (!controls || !section) {
                return
            }
            section.classList.add('das-show-hide__section');
            button.setAttribute('aria-controls', controls);
            button.setAttribute('aria-expanded', sectionExpanded);
            button.removeAttribute('data-aria-controls');
            button.addEventListener('click', that.handleButtonClick.bind(that));
        });

        this.updateButtons(this.buttons, false);

        // Show links - will just show a hidden section - rather than toggling
        nodeListForEach$2(showLinks, function (showLink) {
            var controls = showLink.getAttribute('data-aria-controls');
            var section = document.getElementById(controls);
            var sectionExpanded = that.isExpanded(section);
            if (!controls || !section) {
                return
            }
            section.classList.add('das-show-hide__section');
            showLink.setAttribute('aria-controls', controls);
            showLink.setAttribute('aria-expanded', sectionExpanded);
            showLink.removeAttribute('data-aria-controls');
            showLink.addEventListener('click', that.handleShowLinkClick.bind(that));
        });

    };

    Showhide.prototype.handleButtonClick = function (event) {

        var button = event.target;
        var hasAriaControls = button.getAttribute('aria-controls');

        event.preventDefault();

        if (hasAriaControls) {
            var section = document.getElementById(hasAriaControls),
                isSectionExpanded = this.isExpanded(section);
            if (!isSectionExpanded) {
                this.showSection(section, button);
            } else {
                this.hideSection(section);
            }
            this.updateButtons(this.buttons, !isSectionExpanded);
        }
    };

    Showhide.prototype.handleShowLinkClick = function (event) {
        var showLink = event.target;
        var hasAriaControls = showLink.getAttribute('aria-controls');
        var buttons = document.querySelectorAll('.das-show-hide__button[aria-controls="' + hasAriaControls + '"]');
        event.preventDefault();

        if (hasAriaControls) {
            var section = document.getElementById(hasAriaControls);
            this.showSection(section, showLink, hasAriaControls);
            // Update any additional buttons to show/hide status of section
            this.updateButtons(buttons, true);
        }
    };

    Showhide.prototype.showSection = function (section, control) {
        var focusId = control.getAttribute('data-focus-id'),
            focusIdExists = document.querySelector('#' + focusId);

        // Show the section
        section.classList.add(this.sectionExpandedClass);
        // Focus on element if exists
        if (focusIdExists) {
            focusIdExists.focus();
        }
    };

    Showhide.prototype.hideSection = function (section) {
        section.classList.remove(this.sectionExpandedClass);
    };

    Showhide.prototype.isExpanded = function (section) {
        return section.classList.contains(this.sectionExpandedClass)
    };

    Showhide.prototype.updateButtons = function (buttons, isSectionExpanded) {
        nodeListForEach$2(buttons, function (button) {
            var additionalButtonString = button.getAttribute('data-button-string');
            button.innerHTML = (!isSectionExpanded ? 'Show' : 'Hide') + (additionalButtonString ? ' ' + additionalButtonString : '');
            button.setAttribute('aria-expanded', !isSectionExpanded);
        });
    };

    function nodeListForEach$2(nodes, callback) {
        if (window.NodeList.prototype.forEach) {
            return nodes.forEach(callback)
        }
        for (var i = 0; i < nodes.length; i++) {
            callback.call(window, nodes[i], i, nodes);
        }
    }

    function nodeListForEach$3(nodes, callback) {
        if (window.NodeList.prototype.forEach) {
            return nodes.forEach(callback)
        }
        for (var i = 0; i < nodes.length; i++) {
            callback.call(window, nodes[i], i, nodes);
        }
    }

    function initAll() {

        // Cookie Banner GDS style
        var $cookieBanner = document.querySelector('[data-module="cookie-banner"]');
        if ($cookieBanner != null) {
            new CookieBanner($cookieBanner);
        }

        // Cookie Settings Page
        var $cookieSettings = document.querySelector('[data-module="cookie-settings"]');
        if ($cookieSettings != null) {
            var $cookieSettingsOptions = $cookieSettings.dataset.options;
            new CookieSettings($cookieSettings, $cookieSettingsOptions);
        }

        // GDS v2 Radios
        var $radios = document.querySelectorAll('[data-module="radios"]');
        nodeListForEach$3($radios, function ($radio) {
            new Radios($radio).init();
        });

        // GDS v2 Tabs

        var $tabs = document.querySelectorAll('[data-module="tabs"]');
        nodeListForEach$3($tabs, function ($tabs) {
            new Tabs($tabs).init();
        });

        var $showHide = document.querySelectorAll('[data-module="das-show-hide"]');
        nodeListForEach$3($showHide, function ($showHide) {
            new Showhide($showHide).init();
        });
    }

    exports.initAll = initAll;
    exports.CookieBanner = CookieBanner;
    exports.Radios = Radios;
    exports.Tabs = Tabs;

    Object.defineProperty(exports, '__esModule', { value: true });

})));