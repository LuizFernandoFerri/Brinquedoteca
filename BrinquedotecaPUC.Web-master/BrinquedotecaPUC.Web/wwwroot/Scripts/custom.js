+function ($) {
    'use strict';

    // SIDEBAR PUBLIC CLASS DEFINITION
    // ================================

    var Sidebar = function (element, options) {
        this.$element = $(element)
        this.options = $.extend({}, Sidebar.DEFAULTS, options)
        this.transitioning = null

        if (this.options.parent) this.$parent = $(this.options.parent)
        if (this.options.toggle) this.toggle()
    }

    Sidebar.DEFAULTS = {
        toggle: true
    }

    Sidebar.prototype.show = function () {
        if (this.transitioning || this.$element.hasClass('sidebar-open')) return


        var startEvent = $.Event('show.bs.sidebar')
        this.$element.trigger(startEvent);
        if (startEvent.isDefaultPrevented()) return

        this.$element
            .addClass('sidebar-open')

        this.transitioning = 1

        var complete = function () {
            this.$element
            this.transitioning = 0
            this.$element.trigger('shown.bs.sidebar')
        }

        if (!$.support.transition) return complete.call(this)

        this.$element
            .one($.support.transition.end, $.proxy(complete, this))
            .emulateTransitionEnd(400)
    }

    Sidebar.prototype.hide = function () {
        if (this.transitioning || !this.$element.hasClass('sidebar-open')) return

        var startEvent = $.Event('hide.bs.sidebar')
        this.$element.trigger(startEvent)
        if (startEvent.isDefaultPrevented()) return

        this.$element
            .removeClass('sidebar-open')

        this.transitioning = 1

        var complete = function () {
            this.transitioning = 0
            this.$element
                .trigger('hidden.bs.sidebar')
        }

        if (!$.support.transition) return complete.call(this)

        this.$element
            .one($.support.transition.end, $.proxy(complete, this))
            .emulateTransitionEnd(400)
    }

    Sidebar.prototype.toggle = function () {
        this[this.$element.hasClass('sidebar-open') ? 'hide' : 'show']()
    }

    var old = $.fn.sidebar

    $.fn.sidebar = function (option) {
        return this.each(function () {
            var $this = $(this)
            var data = $this.data('bs.sidebar')
            var options = $.extend({}, Sidebar.DEFAULTS, $this.data(), typeof options == 'object' && option)

            if (!data && options.toggle && option == 'show') option = !option
            if (!data) $this.data('bs.sidebar', (data = new Sidebar(this, options)))
            if (typeof option == 'string') data[option]()
        })
    }

    $.fn.sidebar.Constructor = Sidebar

    $.fn.sidebar.noConflict = function () {
        $.fn.sidebar = old
        return this
    }

    $(document).on('click.bs.sidebar.data-api', '[data-toggle="sidebar"]', function (e) {
        var $this = $(this), href
        var target = $this.attr('data-target')
            || e.preventDefault()
            || (href = $this.attr('href')) && href.replace(/.*(?=#[^\s]+$)/, '')
        var $target = $(target)
        var data = $target.data('bs.sidebar')
        var option = data ? 'toggle' : $this.data()

        $target.sidebar(option)
    })

    $('html').on('click.bs.sidebar.autohide', function (event) {
        var $this = $(event.target);
        var isButtonOrSidebar = $this.is('.sidebar, [data-toggle="sidebar"]') || $this.parents('.sidebar, [data-toggle="sidebar"]').length;
        if (isButtonOrSidebar) {
            return;
        } else {
            var $target = $('.sidebar');
            $target.each(function (i, trgt) {
                var $trgt = $(trgt);
                if ($trgt.data('bs.sidebar') && $trgt.hasClass('sidebar-open')) {
                    $trgt.sidebar('hide');
                }
            })
        }
    });


    $(document).on("click", "#menuMain a.list-group-item", function () {
        if ($(this).attr("target") !== "_blank" && $(this).attr("target") !== "_parent")
            waitingDialog.show("Aguarde....");
    });

    $.fn.extend({
        adjustHeight: function () {
            var element = $(this);
            var finalHeight = 0;
            $.each(element, function (i, compare) {
                if ($(compare).height() > finalHeight) {
                    finalHeight = $(compare).height();
                }
            });
            $.each(element, function (i, change) {
                $(change).height(finalHeight);
            });
            return $(this);
        }
    });    

}(jQuery);

$(document).ready(function () {

    var ajaxReloadSessionClockExecuted = false;

    // #region Session Clock


    $(document).off("ajaxComplete")
        .on("ajaxComplete", function (event, xhr, options) {


            debugger;

            if (xhr !== null && xhr !== undefined)
                if (xhr.getResponseHeader('LOGIN') !== null || (xhr.statusText !== null && xhr.statusText.toLowerCase().indexOf('error') !== -1))
                    window.location.reload(); // Atualizo a pagina para redirecionar para o login

            if (!ajaxReloadSessionClockExecuted) {

                ajaxReloadSessionClockExecuted = true;
                var url = $("#urlRenovarSession").val();

                if (url != null && url.length) {

                    clearTimeout(varSetTimeOut_AtualizaContador);
                    varSetTimeOut_AtualizaContador = 0;

                    $.ajax(
                        {
                            type: 'GET',
                            url: url,
                            async: true,
                            success: function (result) {
                                if (result.success) {
                                    $('#divSessionClock').html(result.view);
                                } else {
                                    $('#toast-session-container').hide();
                                }
                            },
                            beforeSend: function () {
                            },
                            complete: function (XMLHttpRequest) {
                            },
                            error: function (xhr) {
                            }
                        });
                }
            } else {
                ajaxReloadSessionClockExecuted = false;
            }
        });

    // #endregion Session Clock

});
