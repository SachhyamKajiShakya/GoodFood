<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Dish.aspx.cs" Inherits="GoodFood.Dish" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <%-- title --%>
    <div class="row">
        <div class="col-md-4">
            <span style="font-size: xx-large;font-weight: 700;" >Dish Table</span>
        </div>
    </div>

    <%-- sub title --%>
    <div class="row">
        <div class="col-md-4">
            <span style="font-size: medium">View, update and delete dishes</span>
        </div>
    </div>
    <br />
    
    <%-- dish code --%>
    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="Label1" runat="server" Text="Dish Code" Font-Size="Medium"></asp:Label>
        </div>
    </div>
    <%-- dish code text box --%>
    <div class="row">
        <div class="col-md-4">
            <asp:TextBox ID="dishcodeTxt" class="textbox" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" Height="25px" BorderWidth="1px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="dishcodeTxt" ErrorMessage="code cannot be empty" ForeColor="Red" style="font-size: smaller" ValidationGroup="dishGroup"></asp:RequiredFieldValidator> <%-- empty field validation --%>      
        </div>
    </div>

    <%-- expression validation --%>
    <div class="row">
        <div class="col-md-4">
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="invalid input" ControlToValidate="dishcodeTxt" ForeColor="Red" style="font-size: smaller" ValidationGroup="dishGroup" ValidationExpression="^[A-Z]+$"></asp:RegularExpressionValidator>
        </div>
    </div>
    
    <%-- dish name --%>
    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="Label2" runat="server" Text="Dish Name"></asp:Label>
        </div>
    </div>

    <%-- dish name text field --%>
    <div class="row">
        <div class="col-md-4">
            <asp:TextBox ID="dishnameTxt" class="textbox" runat="server" BorderColor="#CCCCCC" BorderStyle="Solid" Height="25px" BorderWidth="1px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="dishnameTxt" ErrorMessage="name cannot be empty" ForeColor="Red" style="font-size: smaller" ValidationGroup="dishGroup"></asp:RequiredFieldValidator><%-- empty field validation--%>
        </div>
    </div>

    <%-- expression validation --%>
    <div class="row">
        <div class="col-md-4">
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="invalid input" ControlToValidate="dishnameTxt" ForeColor="Red" style="font-size: smaller" ValidationGroup="dishGroup" ValidationExpression="^([a-zA-Z]+\s*)+$"></asp:RegularExpressionValidator>
        </div>
    </div>

    <%-- local name --%>
    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="Label3" runat="server" Text="Local Name"></asp:Label>
        </div>
    </div>

    <%-- local name text box field --%>
    <div class="row">
        <div class="col-md-4">
            <asp:TextBox class="textbox" ID="localnameTxt" runat="server" BorderColor="#CCCCCC" Height="25px" BorderWidth="1px" BorderStyle="Solid"></asp:TextBox>
        </div>
    </div>

    <%-- expression validation --%>
    <div class="row">
        <div class="col-md-4">
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ErrorMessage="invalid input" ValidationExpression="^([a-zA-Z]+\s*)+$" ControlToValidate="localnameTxt" ForeColor="Red" style="font-size: smaller" ValidationGroup="dishGroup"></asp:RegularExpressionValidator>
        </div>
    </div>
        

    <%-- loyality score --%>
    <div class="row">
        <div class="col-md-4">
            <asp:Label ID="Label4" runat="server" Text="Loyalty Score" Font-Size="Smaller" style="font-size: small"></asp:Label>
        </div>
    </div>

    <%-- loyalty score text box --%>
    <div class="row">
        <div class="col-md-4">
            <asp:DropDownList class="textbox" ID="ddlloyaltyscore" runat="server" Height="24px" Width="180px" DataSourceID="SqlDataSource1" DataTextField="LOYALTY_POINTS" DataValueField="LOYALTY_ID"></asp:DropDownList>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" ProviderName="<%$ ConnectionStrings:ConnectionString.ProviderName %>" SelectCommand="SELECT &quot;LOYALTY_ID&quot;, &quot;LOYALTY_POINTS&quot; FROM &quot;LOYALTY_POINTS&quot;"></asp:SqlDataSource>
        </div>
    </div>
    <br />

    <%-- insert button --%>
    <div class="row">
        <div class="col-md-4">
            <asp:Button class="button" ID="insertBtn" runat="server" Text="Insert" OnClick="insertBtn_Click" BackColor="#1877F2" BorderStyle="None" ForeColor="White" ValidationGroup="dishGroup"  />
        </div>
    </div>
    <br />

    <%-- grid view --%>
    <div class="row">
        <div class="col-md-12">
            <asp:GridView cellpadding="8" ID="GridViewDish" runat="server" DataKeyNames="Dish_Code" OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing" OnRowCancelingEdit="OnRowCancelingEdit" OnRowUpdating="OnRowUpdating" OnRowDeleting="OnRowDeleting" EmptyDataText="No records has been added." AutoGenerateEditButton="true" AutoGenerateDeleteButton="true" Width="1094px" Font-Size="Medium" BorderColor="#CCCCCC" BorderStyle="Solid">
            </asp:GridView>
        </div>
    </div>
    
</asp:Content>