<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Available Dishes.aspx.cs" Inherits="GoodFood.Available_Dishes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <%-- title --%>
    <div class="row">
        <div class="col-md-4">
            <span style="font-size: xx-large;font-weight: 700;">Available Dishes</span>
        </div>
    </div>

    <%-- sub title --%>
    <div class="row">
        <div class="col-md-4">
            <span style="font-size: medium">View detail of dishes available</span>
        </div>
    </div>
    <br />


    <div class="row">
        <%-- dish drop down --%>
        <div class="col-md-4">
            <asp:Label ID="Label1" runat="server" Text="Dish"></asp:Label>
            <asp:DropDownList ID="ddldishcode" runat="server" Height="24px" Width="180px" DataSourceID="SqlDataSource1" DataTextField="DISH_NAME" DataValueField="DISH_CODE"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;DISH_CODE&quot;, &quot;DISH_NAME&quot; FROM &quot;DISHES&quot;"></asp:SqlDataSource>
        </div>
        <%-- search button --%>
        <div class="col-md-4">
            <asp:Button class="button" ID="searchbtn" runat="server" Text="Search" BackColor="#1877F2" BorderStyle="None" BorderWidth="1px" OnClick="searchbtn_Click"/>
        </div>
    </div>

     <br />
    <br />

    <%-- grid view --%>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView cellpadding="10" ID="GridViewAvailableDishes" runat="server" EmptyDataText="The dish is not available in any restaurant for now." Width="1300px" Font-Size="Small" BorderColor="#CCCCCC" BorderStyle="Solid"></asp:GridView>
        </div>
    </div>
</asp:Content>