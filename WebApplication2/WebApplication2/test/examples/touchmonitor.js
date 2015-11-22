/*基于touchwipe修改，touchwipe使用不方便*/
var touchmonitor = function (c) {
    var b = {
        obj: [],
        drag: false,
        min_move_x: 20,
        min_move_y: 20,
        wipeLeft: function () { },
        wipeRight: function () { },
        wipeUp: function () { },
        wipeDown: function () { },
        wipe: function () {/*点击*/ },
        wipehold: function () {/*触摸保持*/ },
        wipeDrag: function (x, y) {/*拖动*/ },
        preventDefaultEvents: true
    };
    if (c) {
        b.obj = c.obj || b.obj;
        b.drag = c.drag || b.drag;
        b.min_move_x = c.min_move_x || b.min_move_x;
        b.min_move_y = c.min_move_y || b.min_move_y;
        b.wipeLeft = c.wipeLeft || b.wipeLeft;
        b.wipeRight = c.wipeRight || b.wipeRight;
        b.wipeUp = c.wipeUp || b.wipeUp;
        b.wipeDown = c.wipeDown || b.wipeDown;
        b.wipe = c.wipe || b.wipe;
        b.wipehold = c.wipehold || b.wipehold;
        b.wipeDrag = c.wipeDrag || b.wipeDrag;
        b.preventDefaultEvents = c.preventDefaultEvents || b.preventDefaultEvents;
    };
    var object = b.obj;
    var h, g, j = false, i = false, e;
    var supportTouch = "ontouchstart" in document.documentElement;
    var moveEvent = supportTouch ? "touchmove" : "mouseover",
    startEvent = supportTouch ? "touchstart" : "mousedown",
    endEvent = supportTouch ? "touchend" : "mouseup";
    var getObj = function () {
        return b.obj;
    };

    var self = {
        /* 移除 touchmove 监听 */
        m: function () {
            object.removeEventListener(moveEvent, self.d);
            h = null;
            j = false;
            clearTimeout(e);
        },

        /* 事件处理方法 */
        d: function (q) {
            if (b.preventDefaultEvents) {
                q.preventDefault();
            };
            if (j) {
                var n = supportTouch ? q.touches[0].pageX : q.pageX;
                var r = supportTouch ? q.touches[0].pageY : q.pageY;
                var p = h - n;
                var o = g - r;
                if (b.drag) {
                    h = n;
                    g = r;
                    clearTimeout(e);
                    b.wipeDrag(p, o);
                }
                else {
                    if (Math.abs(p) >= b.min_move_x) {
                        self.m();
                        if (p > 0) { b.wipeLeft() }
                        else { b.wipeRight() }
                    }
                    else {
                        if (Math.abs(o) >= b.min_move_y) {
                            self.m();
                            if (o > 0) { b.wipeUp() }
                            else { b.wipeDown() }
                        }
                    }
                }
            }
        },

        /*wipe 处理方法*/
        k: function () {
            clearTimeout(e);
            if (!i && j) {
                b.wipe();
            }
            i = false;
            j = false;
        },
        /*wipehold 处理方法*/
        l: function () { i = true; b.wipehold() },

        f: function (n) {
            //if(n.touches.length==1){
            h = supportTouch ? n.touches[0].pageX : n.pageX;
            g = supportTouch ? n.touches[0].pageY : n.pageY;
            j = true;
            e = setTimeout(self.l, 750)
            //}
        },
        init: function () {
            object.addEventListener(startEvent, self.f, false);
            object.addEventListener(moveEvent, self.d, false);
            object.addEventListener(endEvent, self.k, false);
        }
    };
    return self.init();
};