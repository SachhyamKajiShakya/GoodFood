<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Order History.aspx.cs" Inherits="GoodFood.Order_History" %>

<asp:content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <%-- title --%>
     <div class="row">
         <div class="col-md-6">
             <span style="font-size: xx-large;font-weight: 700;">Top 5 restaurants of the month</span>
         </div>
    </div>

    <%-- sub title --%>
    <div class="row">
        <div class="col-md-4">
            <span style="font-size: medium">View details of top 5 restaurant of the month</span>
        </div>
    </div>

    <br />
    <br />

    <%-- view button --%>
    <div class="row">
        <div class="col-md-6">
            <asp:Label ID="monthLbl" runat="server" Text="Enter the month: "></asp:Label>
            <asp:TextBox ID="monthTxt" class="textbox" BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" Height="25px" runat="server" ValidationGroup="orderGroup"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="monthTxt" ErrorMessage="month cannot be empty" ForeColor="Red" style="font-size: smaller" ValidationGroup="orderGroup"></asp:RequiredFieldValidator>
        </div>

        <%-- view button --%>
        <div class="col-md-4">
            <asp:Button ID="searchBtn" runat="server" Text="View"  BackColor="#1877F2" BorderColor="#1877F2" ForeColor="White" BorderStyle="None" class="button" OnClick="searchBtn_Click" ValidationGroup="orderGroup" />
        </div>
    </div>

    <%-- regular expression validator for month --%>
    <div class="row">
        <div class="col-md-4">
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="monthTxt" ErrorMessage="please enter valid year/month" ForeColor="Red" style="font-size: smaller" ValidationGroup="orderGroup" ValidationExpression="^[0-9]{4}[/][0-9]{2}$"></asp:RegularExpressionValidator>
        </div>
    </div>

    <br />

    <%-- grid view --%>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView cellpadding="10" ID="GridViewOrderHistory" EmptyDataText="No entries are present for the month" Width="1300px" Font-Size="Small" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid"></asp:GridView>
        </div>
    </div>
</asp:content>