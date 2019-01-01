const Utilities = {
    ParseToRem : function (pixels) {
        return pixels * parseFloat(getComputedStyle(document.documentElement).fontSize)
    }
}