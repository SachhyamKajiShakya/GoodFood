<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Loyalty.aspx.cs" Inherits="GoodFood.Loyalty" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <%-- title --%>
     <div class="row">
         <div class="col-md-4">
             <span style="font-size: xx-large;font-weight: 700;">Loyalty Table</span>
         </div>
    </div>

    <%-- sub title --%>
    <div class="row">
        <div class="col-md-4">
            <span style="font-size: medium">View, update and delete loyalties</span>
        </div>
    </div>
    <br />

    <%-- loyalty point --%>
    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="Label1" runat="server" Text="Loyalty Point"></asp:Label>
        </div>
    </div>


    <%-- loyalty point text field --%>
    <div class="row">
        <div class="col-md-4">
            <asp:TextBox ID="pointTxt" class="textbox" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" Height="25px" BorderWidth="1px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="pointTxt" ErrorMessage="point cannot be empty" ForeColor="Red" style="font-size: smaller" ValidationGroup="loyaltyGroup"></asp:RequiredFieldValidator>
        </div>
    </div>
    <br />
    <%-- start date --%>
    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="Label2" runat="server" Text="Start Date"></asp:Label>
        </div> 
    </div>

    <%-- start date text field --%>
    <div class="row">
        <div class="col-md-6">
            <asp:TextBox ID="startdateTxt" class="textbox" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" Height="25px" BorderWidth="1px"></asp:TextBox>
            <asp:ImageButton ID="startDateBtn" runat="server" ImageUrl="~/calendar.png" Height="20px" OnClick="startDateBtn_Click" ImageAlign="AbsMiddle" /><%-- adding image button for calendar--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="startdateTxt" ErrorMessage="date cannot be empty" ForeColor="Red" style="font-size: smaller" ValidationGroup="loyaltyGroup"></asp:RequiredFieldValidator>
            <%-- calendar for picking start date --%>
            <asp:Calendar ID="startDateCalendar" runat="server" Height="200px" Width="220px" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" ShowGridLines="True" OnSelectionChanged="startDateCalendar_SelectionChanged">
                <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                <OtherMonthDayStyle ForeColor="#CC9966" />  
                <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                <SelectorStyle BackColor="#FFCC66" />
                <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
            </asp:Calendar>
        </div>
    </div>
    <br />

    <%-- end date --%>
    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="Label3" runat="server" Text="End Date"></asp:Label>
        </div>
    </div>

    <%-- end date textfield --%>
    <div class="row">
        <div class="col-md-4">
            <asp:TextBox ID="enddateTxt" class="textbox" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" Height="25px" BorderWidth="1px"></asp:TextBox>
            <asp:ImageButton ID="endDateBtn" runat="server" ImageUrl="~/calendar.png" Height="20px" OnClick="endDateBtn_Click" ImageAlign="AbsMiddle"/> <%-- adding image button for calendar--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="enddateTxt" ErrorMessage="date cannot be empty" ForeColor="Red" style="font-size: smaller" ValidationGroup="loyaltyGroup"></asp:RequiredFieldValidator>
            <%-- calendar for entering picking end date --%>
            <asp:Calendar ID="endDateCalendar" runat="server" BackColor="#FFFFCC" BorderColor="#FFCC66" BorderWidth="1px" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#663399" Height="200px" ShowGridLines="True" Width="220px" OnSelectionChanged="endDateCalendar_SelectionChanged">
                <DayHeaderStyle BackColor="#FFCC66" Font-Bold="True" Height="1px" />
                <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC" />
                <OtherMonthDayStyle ForeColor="#CC9966" />
                <SelectedDayStyle BackColor="#CCCCFF" Font-Bold="True" />
                <SelectorStyle BackColor="#FFCC66" />
                <TitleStyle BackColor="#990000" Font-Bold="True" Font-Size="9pt" ForeColor="#FFFFCC" />
                <TodayDayStyle BackColor="#FFCC66" ForeColor="White" />
            </asp:Calendar>
        </div>
    </div>
    <br />

    <%-- insert button --%>
    <div class="row">
        <div class="col-md-4">
            <asp:Button class="button" ID="insertBtn" runat="server" Text="Insert" OnClick="insertBtn_Click" BackColor="#1877F2" BorderColor="#1877F2" BorderStyle="None" ForeColor="White" ValidationGroup="loyaltyGroup"  />
        </div>
    </div>
    <br />

    <%-- grid view --%>
    <div class="row">
        <div class="col-md-4">
            <asp:GridView cellpadding="8" ID="GridViewLoyalty" runat="server" DataKeyNames="loyalty_id" OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added." AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" Width="1094px" Font-Size="Medium" BorderColor="#CCCCCC" BorderStyle="Solid">
            </asp:GridView>
        </div>
    </div>

</asp:Content>
