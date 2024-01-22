using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Http;


namespace ContentEditableMVCNetCore
{
    /// <summary>
    /// Contains the ContentEditableMvc extension methods for the Html Helper.
    /// ContentEditableFor should follow the parameter pattern:
    /// ActionName ControllerName ModelProperty ModelData EnableEditing
    /// </summary>
    public static class ContentEditableMvcExtensions
    {
        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <returns>
        /// The Html for the content, which can be edited.
        /// </returns>
        public static IHtmlContent ContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName, string controllerName,
                                                        Expression<Func<TModel, TProperty>>  expression, object modelData, bool enableEditing)
        {
            return InternalContentEditableFor(htmlHelper, actionName, controllerName, expression, modelData, enableEditing, false,false, false, null);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlContent ContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName, string controllerName,
                                                        Expression<Func<TModel, TProperty>> expression, object modelData)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, controllerName, expression, modelData, true, false,false, false, null);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlContent ContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName, 
                                                        Expression<Func<TModel, TProperty>> expression, object modelData, bool enableEditing)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, null, expression, modelData, enableEditing, false,false, false, null);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlContent ContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName,
                                                        Expression<Func<TModel, TProperty>> expression, object modelData)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, null, expression, modelData, true, false,false, false, null);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlContent DateContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName,
                                                        Expression<Func<TModel, TProperty>> expression, object modelData)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, null, expression, modelData, true, false, false, true, null);
        }

        /// <summary>
        /// Creates a Multiline Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlContent MultilineContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName, string controllerName,
                                                        Expression<Func<TModel, TProperty>> expression, object modelData, bool enableEditing)
        {
            return InternalContentEditableFor(htmlHelper, actionName, controllerName, expression, modelData, enableEditing, true,false, false, null);
        }

        /// <summary>
        /// Creates a Multiline Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlContent MultilineContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName, string controllerName,
                                                        Expression<Func<TModel, TProperty>> expression, object modelData)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, controllerName, expression, modelData, true, true,false, false, null);
        }

        /// <summary>
        /// Creates a Multiline Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlContent MultilineContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName,
                                                        Expression<Func<TModel, TProperty>> expression, object modelData, bool enableEditing)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, null, expression, modelData, enableEditing, true,false, false, null);
        }

        /// <summary>
        /// Creates a Multiline Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlContent MultilineContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName,
                                                        Expression<Func<TModel, TProperty>> expression, object modelData)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, null, expression, modelData, true, true,false, false, null);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content thanks to a dropdown menu.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <param name="selectList">the select list items for the dropdown menu</param>
        /// <returns>
        /// The Html for the content, which can be edited.
        /// </returns>
        public static IHtmlContent DropDownContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName, string controllerName,
                                                        Expression<Func<TModel, TProperty>> expression, object modelData, bool enableEditing,IEnumerable<SelectListItem> selectList)
        {
            return InternalContentEditableFor(htmlHelper, actionName, controllerName, expression, modelData, enableEditing, false,true, false, selectList);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content thanks to a dropdown menu.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="selectList">the select list items for the dropdown menu</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlContent DropDownContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName, string controllerName,
                                                        Expression<Func<TModel, TProperty>> expression, object modelData,IEnumerable<SelectListItem> selectList)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, controllerName, expression, modelData, true, false,true, false, selectList);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content thanks to a dropdown menu.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <param name="selectList">the select list items for the dropdown menu</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlContent DropDownContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName,
                                                        Expression<Func<TModel, TProperty>> expression, object modelData, bool enableEditing,IEnumerable<SelectListItem> selectList)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, null, expression, modelData, enableEditing, false,true, false, selectList);
        }

        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content thanks to a dropdown menu.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that 
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="selectList">the select list items for the dropdown menu</param>
        /// <returns>The Html for the content, which can be edited.</returns>
        public static IHtmlContent DropDownContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName,
                                                        Expression<Func<TModel, TProperty>> expression, object modelData,IEnumerable<SelectListItem> selectList)
        {
            //  Call the main function.
            return InternalContentEditableFor(htmlHelper, actionName, null, expression, modelData, true, false,true, false, selectList);
        }


        /// <summary>
        /// Creates a Content Editable element, that allows the user to edit content inline.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TProperty">The type of the property.</typeparam>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <param name="actionName">Name of the action to call to save changes.</param>
        /// <param name="controllerName">Name of the controller to use to save changes.</param>
        /// <param name="expression">The expression that selects the model property that will be editable.</param>
        /// <param name="modelData">The model data, which is passed to the action. This would typically be soemthing that
        /// identifies the model, such as new { id = Model.Id }.</param>
        /// <param name="enableEditing">if set to <c>true</c> enable editing, otherwise, display the content
        /// as standard read-only text.</param>
        /// <param name="allowMultiline">if set to <c>true</c> allow multiline.</param>
        /// <param name="isDropDown">if set to <c>true</c> allow dropdown menu.</param>
        /// <param name="selectList">Selection list in order to populate the object in case of dropdown choice.</param>
        /// <returns>
        /// The Html for the content, which can be edited.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// An Action Name must be specified to allow content to be edited.
        /// or
        /// An expression that selects the model property must be provided.
        /// </exception>
        private static IHtmlContent InternalContentEditableFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, string actionName, string controllerName,
                                                                 Expression<Func<TModel, TProperty>> expression, object modelData, bool enableEditing,
                                                                 bool allowMultiline,bool isDropDown, bool isDate, IEnumerable<SelectListItem> selectList)
        {
            //  First, we'll create the URL to the action.
            if (string.IsNullOrEmpty(actionName))
                throw new ArgumentException("An Action Name must be specified to allow content to be edited.", "actionName");

            var urlHelper = new UrlHelper(htmlHelper.ViewContext);

            //  Create the action URL, optionally using the controller name.
            var actionUrl = string.IsNullOrEmpty(controllerName)
                                   ? urlHelper.Action(actionName) // (new UrlHelper(htmlHelper.ViewContext.HttpContext)).Action(actionName)
                                   : urlHelper.Action(actionName, controllerName); // (new UrlHelper(htmlHelper.ViewContext.RequestContext)).Action(actionName, controllerName);

            //  Get the editable content.
            if (expression == null)
                throw new ArgumentException("An expression that selects the model property must be provided.", "expression");
            var expressionResult = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider).Model;
            var editableContent = expressionResult != null ? expressionResult.ToString() : string.Empty;
            var editableContentPropertyName = ExpressionHelper.GetExpressionText(expression);

            //  If we are not editable, we can simply return the editable content, without wrapping it in a span.
            if (enableEditing == false)
                return new HtmlString(editableContent);

            //  Create the model data json.
            var modelDataJson = string.Empty;
            if (modelData != null)
                modelDataJson = (JsonConvert.SerializeObject(modelData));

            //  Return the Content Editable element.
            return CreateContentEditableHtml(actionUrl, editableContentPropertyName, editableContent, modelDataJson, allowMultiline,isDropDown,isDate,selectList);
        }

        /// <summary>
        /// Creates the content editable HTML.
        /// </summary>
        /// <param name="actionUrl">The action URL.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="content">The content.</param>
        /// <param name="modelDataJson">The model data json.</param>
        /// <param name="allowMultiline">if set to <c>true</c> allow multiline.</param>
        /// <param name="isDropDown">if set to <c>true</c> allow dropdown menu.</param>
        /// <param name="selectList">select list item for the dropdown (can be null)</param>
        /// <returns>Html for the content editable element.</returns>
        private static IHtmlContent CreateContentEditableHtml(string actionUrl, string propertyName, string content, string modelDataJson, bool allowMultiline,bool isDropDown, bool isDate, IEnumerable<SelectListItem> selectList)
        {
            var savechanges = new TagBuilder("a");
            savechanges.AddCssClass("cem-savechanges");
            savechanges.Attributes["href"] = "#";

            var discardchanges = new TagBuilder("a");
            discardchanges.AddCssClass("cem-discardchanges");
            discardchanges.Attributes["href"] = "#";

            var toolbar = new TagBuilder("div");
            toolbar.AddCssClass("cem-toolbar");
            toolbar.InnerHtml.Append(discardchanges.ToString());
            toolbar.InnerHtml.Append(savechanges.ToString());

            if (isDropDown)
            {
                TagBuilder dropDownSelection = new TagBuilder("select");
                dropDownSelection.AddCssClass("cem-dropdownbox");
                foreach (SelectListItem el in selectList)
                {
                    TagBuilder option = new TagBuilder("option");
                    option.Attributes["value"]=el.Value;
                    option.InnerHtml.Append(el.Text);
                    dropDownSelection.InnerHtml.AppendHtml(option);
                }
                toolbar.InnerHtml.Append(dropDownSelection.ToString());
            }

            if (isDate)
            {
                TagBuilder dateSelection = new TagBuilder("input");

                dateSelection.AddCssClass("cem-datepicker");
                //foreach (SelectListItem el in selectList)
                //{
                //    TagBuilder option = new TagBuilder("option");
                //    option.Attributes["value"] = el.Value;
                //    option.InnerHtml.Append(el.Text);
                //    dropDownSelection.InnerHtml.AppendHtml(option);
                //}

                dateSelection.Attributes.Add("type", "date");

                toolbar.InnerHtml.Append(dateSelection.ToString());
            }

            var contenteditable = new TagBuilder("div");
            contenteditable.Attributes["contenteditable"] = "true";
            contenteditable.AddCssClass("cem-content");
            contenteditable.Attributes["data-property-name"] = propertyName;
            contenteditable.Attributes["data-edit-url"] = actionUrl;
            contenteditable.Attributes["data-model-data"] = modelDataJson;
            contenteditable.Attributes["data-multiline"] = allowMultiline ? "true" : "false";
            contenteditable.Attributes["data-dropdown"] = isDropDown ? "true" : "false";
            contenteditable.InnerHtml.Append(content);

            var wrapper = new TagBuilder("div");
            wrapper.AddCssClass("cem-wrapper");

            wrapper.InnerHtml.Append(contenteditable.ToString());
            wrapper.InnerHtml.Append(toolbar.ToString());

            return new HtmlString(wrapper.ToString());
        }
    }
}