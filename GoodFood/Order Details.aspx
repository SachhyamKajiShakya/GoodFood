<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Order Details.aspx.cs" Inherits="GoodFood.Order_Details" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <%-- title --%>
      <div class="row">
          <div class="col-md-4">
              <span style="font-size: xx-large;font-weight: 700;">Customer Order Table</span>
          </div>
    </div>

    <%-- sub title --%>
    <div class="row">
        <div class="col-md-4">
                <span style="font-size: medium">View detail of orders made by customers</span>
        </div>
    </div>
    <br />
    <br />

    <div class="row">
        <div class="col-md-4">
               <%-- customer drop down --%>
            <asp:Label ID="Label1" runat="server" Text="Customer"></asp:Label>
            <asp:DropDownList ID="ddlcustomer" runat="server" Height="24px" Width="180px" DataSourceID="SqlDataSource1" DataTextField="CUSTOMER_NAME" DataValueField="CUSTOMER_ID"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;CUSTOMER_ID&quot;, &quot;CUSTOMER_NAME&quot; FROM &quot;CUSTOMERS&quot;"></asp:SqlDataSource>
        </div>
    <%-- delivery address drop down --%>
        <div class="col-md-4">
            <asp:Label ID="Label2" runat="server" Text="Delivery Address"></asp:Label>
            <asp:DropDownList ID="ddladdress" Height="24px" Width="180px" runat="server" DataSourceID="SqlDataSource2" DataTextField="DELIVERY_POINT" DataValueField="DELIVERY_ADDRESS_ID"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;DELIVERY_ADDRESS_ID&quot;, &quot;DELIVERY_POINT&quot; FROM &quot;DELIVERY_ADDRESS&quot;"></asp:SqlDataSource>
        </div>

        <%-- view button --%>
        <div class="col-md-4">
            <asp:Button class="button" ID="searchbtn" runat="server" Text="View" BackColor="#1877F2" BorderStyle="None" BorderWidth="1px" OnClick="searchbtn_Click" />
        </div>
    </div>

    <br />
    <br />

    <%-- grid view --%>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView cellpadding="10" ID="GridViewOrderDetails" runat="server" EmptyDataText="Booking with such credentials have not been made." Width="1300px" Font-Size="Small" BorderColor="#CCCCCC" BorderStyle="Solid"></asp:GridView>
        </div>
    </div>
</asp:Content>