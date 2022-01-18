<%@ Page Title="" Language="C#" MasterPageFile="~/App_MasterLayout/MasterPage_main.master"
    AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="main" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="<%=ResolveUrl("~/js/gauge.min.js")%>"></script>
    <script src="<%=ResolveUrl("~/js/500net/500net.main.js")%>"></script>
    <script>
        $(document).ready(function () {
            $.ajax({
                type: "POST",
                url: "Functions/myWebService.asmx/GetCarAmount",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json", // dataType is json format
                success: OnSuccess,
                error: OnErrorCall
            });

        });

        function OnSuccess(response) {
            console.log(response.d)
            var aData = response.d;

            var aDatasets1 = aData[0];
            //console.log(aDatasets1)

            var xxx = 0;
            var yyy = 0;
            $.each(aDatasets1, function (i) {

                var totalObj = $('#s_' + aDatasets1[i].floor)


                $(totalObj).text(aDatasets1[i].amount + '/' + aDatasets1[i].totalamount)

                if (aDatasets1[i].floor.toString().includes("Vendor")) {

                } else {
                    xxx += parseInt(aDatasets1[i].amount);
                    yyy += parseInt(aDatasets1[i].totalamount);
                }


            });
            var zzz = $('#s_total')
            $(zzz).text(xxx + '/' + yyy)
        }

        function OnErrorCall(response) {
            console.log(response);
        }
		
    </script>
</asp:Content>
<asp:Content ID="Content_content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <hgroup id="main_title" class="thin" style="margin-left: 10px;">
        <h2>
            資訊看板 &nbsp;
            <asp:Literal ID="Li_Month" runat="server"></asp:Literal>
            <strong>
                <asp:Literal ID="Li_Day" runat="server"></asp:Literal>
            </strong>
        </h2>
    </hgroup>
    <div class="dashboard">
        <div class="columns" style="width: inherit">
            <div class="nine-columns twelve-columns-mobile" id="demo-chart" style="display:none" >
                <!-- This div will hold the chart generated in the footer -->
                <asp:Chart ID="Chart1" runat="server" BackColor="Black" BackGradientStyle="TopBottom"
                    BackSecondaryColor="White" BorderColor="#FFFFFF" BorderlineDashStyle="Solid"
                    BorderWidth="2px" Width="900px" Height="400px" OnLoad="Chart1_Load1">
                    <Titles>
                        <asp:Title Font="Times New Roman, 14pt, style=Bold" ForeColor="White" ShadowColor="32,0,0,0"
                            ShadowOffset="3" Text="折線圖" Name="Title1">
                        </asp:Title>
                    </Titles>
                    <Series>
                        <asp:Series BorderColor="180, 26, 59, 105" ChartArea="LineChartArea" CustomProperties="LabelStyle=Bottom"
                            IsValueShownAsLabel="false" LabelFormat="" Name="1F" XValueType="DateTime" XValueMember="xline"
                            YValueMembers="yline" ChartType="Line" BorderWidth="3" Legend="違規">
                        </asp:Series>
                        <asp:Series BorderColor="180, 55, 59, 105" ChartArea="LineChartArea" CustomProperties="LabelStyle=Bottom"
                            IsValueShownAsLabel="false" LabelFormat="" Name="B1F" XValueType="DateTime" XValueMember="xline"
                            YValueMembers="yline1" ChartType="Line" BorderWidth="3" Legend="違規">
                        </asp:Series>
                        <asp:Series BorderWidth="3" ChartArea="LineChartArea" ChartType="Line" IsValueShownAsLabel="false"
                            Legend="違規" Name="B2F" XValueMember="xline" XValueType="DateTime" YValueMembers="yline2">
                        </asp:Series>
                        <asp:Series BorderColor="180, 26, 95, 105" ChartArea="LineChartArea" CustomProperties="LabelStyle=Bottom"
                            IsValueShownAsLabel="false" LabelFormat="" Name="B3F" XValueType="DateTime" XValueMember="xline"
                            YValueMembers="yline3" ChartType="Line" BorderWidth="3" Legend="違規">
                        </asp:Series>
                        <asp:Series BorderColor="120, 26, 59, 105" ChartArea="LineChartArea" CustomProperties="LabelStyle=Bottom"
                            IsValueShownAsLabel="false" LabelFormat="" Name="B4F" XValueType="DateTime" XValueMember="xline"
                            YValueMembers="yline4" ChartType="Line" BorderWidth="3" Legend="違規">
                        </asp:Series>
                    </Series>
                    <Legends>
                        <asp:Legend Name="違規" Title="停車數" BackColor="224, 224, 224" BackGradientStyle="TopBottom"
                            BackImageAlignment="Left" BackImageTransparentColor="White">
                            <Position Height="16" Width="40.7969933" />
                        </asp:Legend>
                    </Legends>
                    <BorderSkin SkinStyle="Emboss" />
                    <ChartAreas>
                        <asp:ChartArea BackColor="200,200,200,200" BackGradientStyle="TopBottom" BackSecondaryColor="White"
                            BorderColor="64,64,64,64" BorderDashStyle="Solid" Name="LineChartArea" ShadowColor="Transparent">
                            <Area3DStyle Inclination="15" IsClustered="false" IsRightAngleAxes="false" Perspective="10"
                                Rotation="10" WallWidth="0" />
                            <AxisY ArrowStyle="Triangle" IsLabelAutoFit="false" LineColor="64,64,64,64">
                                <LabelStyle Font="Times New Roman, 10pt, style=Bold" ForeColor="White" />
                                <MajorGrid LineColor="64,64,64,64" />
                            </AxisY>
                            <AxisX ArrowStyle="Triangle" IsLabelAutoFit="false" LineColor="255,64,64,64">
                                <LabelStyle ForeColor="24,24,24" Font="Times New Roman, 10pt, style=Bold" IsStaggered="true" />
                                <MajorGrid LineColor="255,64,64,64" />
                            </AxisX>
                        </asp:ChartArea>
                    </ChartAreas>
                </asp:Chart>
            </div>
           
            <div class="three-columns twelve-columns-mobile new-row-mobile">
                <h1>
                    員工停車區</h1>
                <ul class="stats split-on-mobile">
                    <li>
                        <div id="div3">
                            <strong><span id="s_B1F">200/200</span></strong> B1F
                            <br />
                        </div>
                    </li>
                    <li>
                        <div id="div5">
                            <strong><span id="s_B2F">300/300</span></strong> B2F
                            <br />
                        </div>
                    </li>
                    <li>
                        <div>
                            <strong><span id="s_B3F">300/300</span></strong> B3F
                            <br />
                        </div>
                    </li>
                    <li>
                        <div>
                            <strong><span id="s_B4F">300/300</span></strong> B4F
                            <br />
                        </div>
                    </li>
                    <li>
                        <div id="div7">
                            <strong><span id="s_total">600/600</span></strong>
                            <br />
                            總車位數
                        </div>
                    </li>
                </ul>
                <h1>
                    廠商停車區</h1>
                <ul class="stats split-on-mobile">
                    <li>
                        <div id="div1">
                            <strong><span id="s_1FVendor">200/200</span></strong> 機車
                            <br />
                        </div>
                    </li>
                    <li>
                        <div id="div2">
                            <strong><span id="s_B4FVendor">300/300</span></strong> 汽車
                            <br />
                        </div>
                    </li>
                </ul>
            </div>

             <div id="flow-chart" class="nine-columns" style="height: 500px;">
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="Server">
    <%--<ul class="unstyled-list">
        <li class="title-menu">今日重要行事曆</li>
        <li>
            <ul class="calendar-menu">
                <li><a href="#" title="See event">
                    <time datetime="2011-02-24"><b>31</b> Dec</time>
                    <small class="green">10:30</small> 動態攝影設備維護作業 </a></li>
            </ul>
        </li>
        <li class="title-menu">今日新增訊息留言</li>
        <li>
            <ul class="message-menu">
                <li><span class="message-status"><a href="#" class="starred" title="Starred">Starred</a>
                    <a href="#" class="new-message" title="Mark as read">New</a> </span><span class="message-info">
                        <span class="blue">10:12</span> <a href="#" class="attach" title="Download attachment">
                            Attachment</a> </span><a href="#" title="Read message"><strong class="blue">Admin Test</strong><br />
                                <strong>請協調CCTV廠商進場事宜</strong> </a></li>
                <li><a href="#" title="Read message"><span class="message-status"><span class="unstarred">
                    Not starred</span> <span class="new-message">New</span> </span><span class="message-info">
                        <span class="blue">09:47</span> </span><strong class="blue">Sam Huang</strong><br />
                    <strong>帳號密碼無法登入</strong> </a></li>
                <li><span class="message-status"><span class="unstarred">Not starred</span> </span><span
                    class="message-info"><span class="blue">08:52</span> </span><strong class="blue">Sam
                        Huang</strong><br />
                    請收訊息 </li>
            </ul>
        </li>
    </ul>--%>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder4" runat="Server">
    <!-- This is optional -->
    <%--<footer id="menu-footer">
        <p class="button-height">
            <input type="checkbox" name="auto-refresh" id="auto-refresh" checked="checked" class="switch float-right" />
            <label for="auto-refresh">
                Auto-refresh</label>
        </p>
    </footer>--%>
    <script src="<%=ResolveUrl("~/js/libs/ECharts/echarts-all.js")%>"></script>
    <script src="<%=ResolveUrl("~/js/libs/ECharts/theme/macarons.js")%>"></script>
    <script>
        $(document).ready(function () {



        });

        function drawchart(aDatasets) {

            var xAxisData = [];
            var y0AxisData = [];
            var y1AxisData = [];
            var y2AxisData = [];
            var y3AxisData = [];
            var y4AxisData = [];
            var y5AxisData = [];    //Car Vendor
            var y6AxisData = [];    //Moto Vendor


            $.each(aDatasets, function (i) {
                y0AxisData.push([new Date(aDatasets[i]["xline"]), aDatasets[i]["yline"]])
                y1AxisData.push([new Date(aDatasets[i]["xline"]), aDatasets[i]["yline1"]])
                y2AxisData.push([new Date(aDatasets[i]["xline"]), aDatasets[i]["yline2"]])
                y3AxisData.push([new Date(aDatasets[i]["xline"]), aDatasets[i]["yline3"]])
                y4AxisData.push([new Date(aDatasets[i]["xline"]), aDatasets[i]["yline4"]])
                y5AxisData.push([new Date(aDatasets[i]["xline"]), aDatasets[i]["yline5"]])
                y6AxisData.push([new Date(aDatasets[i]["xline"]), aDatasets[i]["yline6"]])
            });


            var myChart = echarts.init(document.getElementById("flow-chart"), theme);
            option = {
                title: {
                    text: '過去一周停車記錄',
                    subtext: '僅供參考'
                },
                tooltip: {
                    trigger: 'item',
                    formatter: function (params) {
                        var date = new Date(params.value[0]);
                        data = date.getFullYear() + '-'
                   + (date.getMonth() + 1) + '-'
                   + date.getDate() + ' '
                   + date.getHours() + ':'
                   + date.getMinutes();
                        return data + '<br/>停放車輛:'
                   + params.value[1] + ' ';
                    }
                },
                dataZoom: {
                    show: true,
                    start: 20
                },
                legend: {
                    data: ['B1F', 'B2F', 'B3F', 'B4F', 'B4F Vendor', '1F Vendor'],
                    textStyle: { color: '#FFFFFF' }
                },
                grid: {
                    y2: 80
                },
                toolbox: {
                    show: true,
                    feature: {
                        mark: { show: false },
                        dataView: { show: false, readOnly: false },
                        magicType: { show: false, type: ['line', 'bar'] },
                        restore: { show: false },
                        saveAsImage: { show: true }
                    }
                },
               
                xAxis: [
                    {
                        type: 'time',
                        splitNumber: 10,
                        axisLabel: {
                            show: true,
                            textStyle: {
                                color: '#FF0000'
                            }
                        }

                    }
                ],
                yAxis: [
                    {
                        type: 'value',

                        axisTick: {
                            show: true,
                            lineStyle: {
                                color: '#FF0000',
                                width: 1
                            }
                        },
                        axisLabel: {
                            show: true,
                            textStyle: {
                                color: '#FF0000'
                            }
                        }


                    }
                ],
                series: [
                     {
                         name: 'B1F',
                         type: 'line',
                         symbol: 'none',
                         data: y1AxisData
                     },
                    {
                        name: 'B2F',
                        type: 'line',
                        symbol: 'none',
                        data: y2AxisData
                    },
                    {
                        name: 'B3F',
                        type: 'line',
                        symbol: 'none',
                        data: y3AxisData
                    },
                    {
                        name: 'B4F',
                        type: 'line',
                        symbol: 'none',
                        data: y4AxisData
                    },
                    {
                        name: 'B4F Vendor',
                        type: 'line',
                        symbol: 'none',
                        data: y5AxisData
                    },
                    {
                        name: '1F Vendor',
                        type: 'line',
                        symbol: 'none',
                        data: y6AxisData
                    }

                ]
            };

            myChart.setOption(option)
        }
    </script>
</asp:Content>
