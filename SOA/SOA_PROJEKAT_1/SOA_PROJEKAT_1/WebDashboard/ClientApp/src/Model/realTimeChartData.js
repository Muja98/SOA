"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
exports.realTimeChartData = void 0;
var realTimeChartData = /** @class */ (function () {
    function realTimeChartData() {
        this.type = "";
        this.typeData = "";
        this.min = 0;
        this.max = 100;
        this.name = "";
        this.realtimeChartData = [[]];
        this.realtimeChartOptions = {
            height: 230,
            width: 400,
            margin: { left: 40 },
            lines: [
                { color: '#34B77C', lineWidth: 3, area: true, areaColor: '#34B77C', areaOpacity: .2 }
            ],
            xGrid: { tickPadding: 15, tickNumber: 5 },
            yGrid: { min: 0, max: 100, tickNumber: 5, tickFormat: function (v) { return v + "C"; }, tickPadding: 25 }
        };
    }
    return realTimeChartData;
}());
exports.realTimeChartData = realTimeChartData;
//# sourceMappingURL=realTimeChartData.js.map