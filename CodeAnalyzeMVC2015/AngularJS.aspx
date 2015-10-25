﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" Inherits="AngularJS" Codebehind="AngularJS.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <p style="font-size:24px;font-weight:bold;color:Black">AngularJS</p> <br />


<ul style="font-size:16px;width:85%;font-family:Calibri;">
<li><a href="#1" style="color:Black">AngularJs and its uses</a></li>
<li><a href="#2" style="color:Black">Advantages of AngularJS</a></li>
<li><a href="#3" style="color:Black">jQLite or jQuery lite in AngularJS</a></li>
<li><a href="#4" style="color:Black">How to access jQuery in AngularJS</a></li>
<li><a href="#5" style="color:Black">AngularJS features</a></li>
<li><a href="#6" style="color:Black">Modules in AngularJS</a></li>
<li><a href="#7" style="color:Black">Expressions in AngularJS</a></li>
<li><a href="#8" style="color:Black">Directives in AngularJS</a></li>
<li><a href="#9" style="color:Black">Filters in AngularJS</a></li>
<li><a href="#10" style="color:Black">Auto bootstrap and Manual bootstrap process in AngularJS</a></li>
<li><a href="#11" style="color:Black">Bootstrap multiple modules with auto and manual methods</a></li>
<li><a href="#12" style="color:Black">Scope in AngularJS</a></li>
<li><a href="#13" style="color:Black">Compiling process in AngularJS</a></li>
<li><a href="#14" style="color:Black">Templates in AngularJS</a></li>
<li><a href="#15" style="color:Black">ng-show, ng-hide, ng-if, ng-switch, ng-repeat, ng-include</a></li>
<li><a href="#16" style="color:Black">Data Binding in Angular JS</a></li>
<li><a href="#17" style="color:Black">Explain $watch, $digest and $apply in AngularJS?</a></li>
<li><a href="#18" style="color:Black">Explain $watch(), $watchgroup() and $watchCollection() in AngularJS?</a></li>

<%--<li><a href="#7" style="color:Black">7. What is difference between config() and run() method in AngularJS?</a></li>
<li><a href="#8" style="color:Black">8. What are some of the inbuilt functions provided in AngularJS?</a></li>
<li><a href="#9" style="color:Black">9. What is AngularJS Prefixes $ and $$?</a></li>--%>
</ul>

<br />

<div style="font-size:16px;width:100%;font-family:Calibri">



    <p>
       <a name="1" style="color: #FF0000">1. AngularJs and its uses</a>
    </p>

        AngularJS is an open-source JavaScript framework from Google. It is used to create single page applications web applications that requires only HTML, CSS, and JavaScript on the client side. It uses MVC pattern. 
        <br /><br />

        There are many uses of AngularJS
        <br />
        1. Extend HTML by attaching our directives to the HTML markup with new attributes, tags, expression to define good templates<br />
        2. Uses MVC pattern which helps in separation of concerns and keeps organized. Canbuild single page applications.<br />
        3. Helps in creating reusable components,create our own directives and abstract your DOM manipulation logic.<br />
        4. Supports two-way data binding which connects HTML views of JavaScript objects models easily. With this any change in model will update the view and vice versa without any DOM manipulation.<br />
        5. Combines the behavior of applicationin controllers which are instantiated with the help of dependency injection.<br />
        6. Supports services that can be injected into controllers to use some utility code to fulfil our need. For example,it provides $http service to communicate with REST service.<br />
        7. Supports dependency injection which helps to test the angular app code easily.<br />
        8. AngularJS is has a very good support on the internet and google community. <br />

        AngularJS represents all public objects with $ and all private objects with $$. 
    

    <br /><br /><br />
    <p>
       <a name="2" style="color: #FF0000">2. Advantages of AngularJS
    </a></p>

     
        Html has angle brackets i.e. <,> andng sound like Angular. That’s why it is called AngularJS.
         <br /><br />
        There are following advantages of AngularJS: <br />
        • Two way data binding<br />
        • Templates<br />
        • Full testing environment<br />
        • Server Communication<br />
        • Deep-Linking<br />
        • Code Reusability <br />
        • Customize & Extensible <br />
        • Testing – Since this is based on dependency injection it helps in great deal for testing and also this designed for good testing. <br />
        • Mobilty – Since this is built on top of javascript, there is no issue of browser compatibility or to integrate with other javascript modules<br />
        • Support – There is good support from various cimmunity and also from Google.<br />


     <br /><br /><br />
    <p>
       <a name="3" style="color: #FF0000">3. jQLite or jQuery lite in AngularJS
    </a></p>

     jQLite can be said as the inherited class or child class of jQuery which is built directly into AngularJS. jQLite contains all the important features of jQuery. However it does not provide all features of jQuery but does provide some of the importnat ones. Here is the list
        <br />
        addClass(), after(), append(), attr(), bind(), children(), clone(), contents(), css(), data(), detach(),
        empty(), eq(), find(), hasClass(), html(), text(), on(), off(), one(), parent(), prepend(), prop(),
        ready(), remove, removeAttr(), removeClass(), removeData(), replaceWith(), toggleClass(), triggerHandler(),
        unbind(), val(), wrap()   
     <br /><br /><br />
  
  
    <p><a name="4" style="color: #FF0000">4. How to access jQuery within AngularJS?
        </a></p>

    jQuery can be accessed within AngularJS by using the element() function. angular.element() is an alias for the jQuery function<br />
    angular.element() === jQuery() === $()

    <pre class="brush: jscript">
        &lt;html&gt;
        &lt;head&gt;
        &lt;script src="lib/angular.js"&gt;&lt;/script&gt;
        &lt;script type="text/javascript"&gt;
            var app = angular.module('app', []);
            app.controller("mainCtrl", function ($scope, $element) {
                $scope.clickme = function () {
                    var elem = angular.element(document.querySelector('#txtName')); console.log(elem.val())
                };
            });
        &lt;/script&gt;
        &lt;/head&gt;

        &lt;body ng-app="app"&gt;
        &lt;div ng-controller="mainCtrl"&gt;
        &lt;input type="text" id="txtName" value="Code Analyze" /&gt;
        &lt;button type="button" ng-click="clickme()"&gt;Click me&lt;/button&gt;
        &lt;/div&gt;
        &lt;/body&gt;
        &lt;/html&gt;
    </pre>


      <br /><br /><br />
    <p><a name="5" style="color: #FF0000">5. AngularJS features
        </a></p>

        <b>Two Way Data-Binding</b>
AngularJS has great two-way data-binding that handles the synchronization between the DOM and the model, and vice versa.
Here is a simple example, which demonstrates how to bind an input value to an element. <br />


        <pre class="brush: jscript">
            &lt;html ng-app&gt;
                &lt;head&gt;
                    &lt;script src="http://code.angularjs.org/angular-1.0.0rc10.min.js"&gt;&lt;/script&gt;
                &lt;/head&gt;
                &lt;body&gt;
                    &lt;div&gt;
                        &lt;label&gt;Name:&lt;/label&gt;
                        &lt;input type="text" ng-model="yourName" placeholder="Enter a name here"&gt;
                        &lt;hr&gt;
                        &lt;h1&gt;Hello, {{yourName}}!&lt;/h1&gt;
                    &lt;/div&gt;
                &lt;/body&gt;
            &lt;/html&gt;
        </pre>
        <br /><br />
        <b>Templates</b>
        In AngularJS, a template is just plain-old-HTML. The HTML vocabulary is extended, to contain instructions on how the model should be projected into the view.
        The HTML templates are parsed by the browser into the DOM. The DOM then becomes the input to the AngularJS compiler. AngularJS traverses the DOM template for rendering instructions, which are called directives. Collectively, the directives are responsible for setting up the data-binding for your application view.
        It is important to realize that at no point does AngularJS manipulate the template as strings. The input to AngularJS is browser DOM and not an HTML string. The data-bindings are DOM transformations, not string concatenations or innerHTMLchanges. 
        <br />Here is an example where I am using the ng-repeat directive to loop over theimages array and populate what is essentially an img template.<br />
        
        
        <pre class="brush :jscript">
        function AlbumCtrl($scope) {
	        scope.images = [
		        {"thumbnail":"img/image_01.png", "description":"Image 01 description"},
		        {"thumbnail":"img/image_02.png", "description":"Image 02 description"},
		        {"thumbnail":"img/image_03.png", "description":"Image 03 description"}
	        ];
        }

        &lt;div ng-controller="AlbumCtrl"&gt;
            &lt;ul&gt;
                &lt;li ng-repeat="image in images"&gt;
                    &lt;img ng-src="{{image.thumbnail}}" alt="{{image.description}}"&gt;
                &lt;/li&gt;
            &lt;/ul&gt;
        &lt;/div&gt;

        </pre>

        <br />
        <b>MVC</b> 
        The MVC or Model-View-Controller pattern means a lot of different things to different people. AngularJS does not implement MVC in the traditional sense, but rather something closer to MVVM (Model-View-ViewModel).<br />
        
        <b>The Model</b>  <br /> The model is simply the data in the application. The model is just plain old JavaScript objects. There is no need to inherit from framework classes, wrap it in proxy objects, or use special getter/setter methods to access it. The fact that we are dealing with vanilla JavaScript is a really nice feature, which cuts down on the application boilerplate.
        
        <br /><b>The ViewModel</b> <br /> A viewmodel is an object that provides specific data and methods to maintain specific views.
        The viewmodel is the $scope object that lives within the AngularJS application.$scope is just a simple JavaScript object with a small API designed to detect and broadcast changes to its state.
        
        <br /><b>The Controller</b> <br /> The controller is responsible for setting initial state and augmenting $scope with methods to control behavior. It is worth noting that the controller does not store state and does not interact with remote services.
        <br />
         Creating Controller 
         <pre class="brush: jscript">
        &lt;script type="text/javascript"&gt; //defining main controller
            app.controller('DemoController', function ($scope) { //defining book viewmodel 
                $scope.Employee = { id: 1, name: 'John', company: 'Company', };
            }); &lt;/script&gt;
        Using Controller 
        &lt;div ng-controller="DemoController"&gt; 
            Id: &lt;span ng-bind="Employee.id"&gt;&lt;/span&gt; &lt;br /&gt; 
            Name:&lt;input type="text" ng-model="Employee.name" /&gt; &lt;br /&gt; 
            Company: &lt;input type="text" ng-model="Employee.company" /&gt; 
        &lt;/div&gt;
        </pre>

        <br /><b>The View</b> <br /> The view is the HTML that exists after AngularJS has parsed and compiled the HTML to include rendered markup and bindings.
        The $scopehas a reference to the data, the controller defines behavior, and the view handles the layout and handing off interaction to the controller to respond accordingly.
        <br />

        <b>Dependency Injection</b> 
        AngularJS has a built-in dependency injection subsystem.
        Dependency Injection (DI) allows you to ask for your dependencies, rather than having to go look for them or make them yourself. 
        To gain access to core AngularJS services, it is simply a matter of adding that service as a parameter; AngularJS will detect that you need that service and provide an instance for you.
 <br />
 
        <pre class="brush :jscript">
          function EditCtrl($scope, $location, $routeParams) {
               // Something clever here...
          }


        You are also able to define your own custom services and make those available for injection as well.
          angular.
              module('MyServiceModule', []).
              factory('notify', ['$window', function (win) {
              return function (msg) {
                  win.alert(msg);
              };
          }]);

          function myController(scope, notifyService) {
              scope.callNotify = function (msg) {
                  notifyService(msg);
              };
          }

     myController.$inject = ['$scope', 'notify'];
     </pre>
     
     <br /><br />
        <b>Directives</b> 
        Directives are my personal favorite feature of AngularJS. Have you ever wished that your browser would do new tricks for you? Well, now it can! This is one of my favorite parts of AngularJS. It is also probably the most challenging aspect of AngularJS.
        Directives can be used to create custom HTML tags that serve as new, custom widgets. They can also be used to "decorate" elements with behavior and manipulate DOM attributes in interesting ways.
        <br />Here is a simple example of a directive that listens for an event and updates its$scope, accordingly.
          <br />
          
          <pre class="brush :jscript">
          myModule.directive('myComponent', function(mySharedService) {
              return {
                  restrict: 'E',
                  controller: function($scope, $attrs, mySharedService) {
                      $scope.$on('handleBroadcast', function() {
                          $scope.message = 'Directive: ' + mySharedService.message;
                      });
                  },
                  replace: true,
                  template: '&lt;input&gt;'
              };
          });
          </pre> 
        Then, you can use this custom directive, like so.
        <pre class="brush :jscript">
        &lt;my-component ng-model="message"&gt;&lt;/my-component&gt;
        </pre> 
        Creating your application as a composition of discrete components makes it incredibly easy to add, update or delete functionality as needed.
       <br />
       
       
       <br />
        <b>Testing</b> 
        AngularJS is written entirely from the ground up to be testable. It even comes with an end-to-end and unit test runner setup. If you would like to see this in action, go check out the angular-seed project at https://github.com/angular/angular-seed.


        <br /> <br />
        <b>Configuration block</b> – This block is executed during the provider registration and configuration phase. We can have these as many as we want in the application. Only providers and constants can be injected into configuration blocks and not instances. This block is used to inject module wise configuration settings to prevent accidental instantiation of services before they have been fully configured. This block is created using config() method.
        <pre class="brush: jscript">
            angular.module('myModule', []). 
            config(function (injectables) 
            { 

            //.....

            }). 
                    run(function (injectables) 
            { 

            //....

            });
        </pre> 

        <b>Run block</b> – This is executed after the execution of configuration block. It inserts instances and constants but not providers. This block is created using run() method. This is similar to main method in C# or Java





       <br /><br /><br />
    <p><a name="6" style="color: #FF0000">6. Modules in AngularJS
        </a></p>


    AngularJS modules are a very important feature of AngularJS. These are the containers which is similar to namespace in Java and C#. AngularJS groups the application code into reusable components or small parts meant to perform specific operation which helps in integration with other applications. Each web page can have a separate module assigned to it via ng-app directive.
    ng is the core module which is loaded in by default when the application is started. This module supplies the basic components for angular app mainly directives, services/factories, filters, global APIs and testing components
    An angular module use configuration and run blocks to inject dependencies like providers, services and constants which get applied to the angular app during the bootstrap process.

    <br />Example of creating module is shown below
    <pre class="brush: jscript">
        &lt;script type="text/javascript"&gt;
            angular.module('myApp', []);
            angular.module('myApp', ['referenceModule1', 'referenceModule2']);
        &lt;/script&gt;


        &lt;html ng-app="myApp"&gt; 
        &lt;head&gt;
                ... 
        &lt;/head&gt; 
        &lt;body&gt; 
                ... 
        &lt;/body&gt;
    </pre> 
    A module might have dependencies on other modules. The dependent modules are loaded by angular before the actual module is loaded.
    In other words the configuration blocks of the dependent modules execute before the configuration blocks of the actual module. The same is true for the run blocks. Each module can only be loaded once, even if multiple other modules require it.
  
   
     <br /><br /><br />
    <p><a name="7" style="color: #FF0000">7. Expressions in AngularJS
    </a></p>  
    
    
    AngularJS expressions are placed inside double braces as: {{expression}}. This are mainly placed inside html templates.
    These are similar javascript expressions.<br />

    {{ i = 0 }}<br />
    {{ x + y }}<br />
    {{ a == b }}<br />
     
    However there are some differences between javascript expressions and AngularJS expressions. <br />

    AngularJS expressions cannot have loops, conditional expressions and exceptions
    Has filters to format data while displaying it in the UI
    Can be added inside HTML templates.
    

      <br /><br /><br />
    <p><a name="8" style="color: #FF0000">8. Directives in AngularJS
    </a></p>  

    <b>Directive</b> - AngularJS directives extend the exisiting HTML elements with additional functions. This will also help in performing data update operations in UI controles, their 
    behaviors and many functions which we usually perform on web page UI.<br /><br />

    Basically it a combination of HTML elments like html controls, markups and atributes along with handling javascript code. Javascript directive code controls the 
    UI template data and behavior of html elements.<br /><br />

    <b>InBuilt Directives</b> - There are some built-in directives provided by AngularJS like as ng-app, ng-controller, ng-repeat, ng-model etc.<br />


    ng-app - Initialize the angular app. 
    ng-init - Initialize the angular app data. 
    ng-model - Bind the html elements like input, select, text area to angular app model.<br /><br />


    <b>Invoking Directives</b> - We can invoke the directive in many ways as follows<br />

    <pre class="brush: jscript">
    As attribute    &lt;span ng-app&gt;&lt;/span&gt; 
    As class        &lt;span class="ng-app: a+b;"&gt;&lt;/span&gt; 
    As element      &lt;ng-app&gt;&lt;/ng-app&gt; 
    As comment      &lt;!-- directive: ng-app a+b--&gt;&lt;br /&gt;&lt;br /&gt;
    </pre> 

    <b>Custom Directive</b>
    Example to create custom directive is as follows<br />

        <pre class="brush: jscript">
    
            var app = angular.module('app', []);
            app.directive("custDir", function () {
            return {
            restrict: "E",                  // this is to define as E-Element, A-Attribute, C-class, M-moment
            scope: {                        // this is the scope
                        title: '@'                      // get attribute value
                    },
            template: "&lt;div&gt;{{ customHtmltemplate }}&lt;/div&gt;",  //define html templates
            templateUrl: 'HtmlPage.html',                       //html page path
            replace: true | false,                          //replace html markup with template yes/no
            transclude: true | false,                       //copy html content  yes/no
            controller: function (scope) {                  //controller definition
                        //.....
                        },

            link: function (scope, element, attrs, controller){  //function for DOM manipulation
                            //.....

                        } 
            } 
            });
        </pre>

        <br />
        Restrict option is to mainly used to declare the way the directive will be invoked in the angular app.<br />
        <pre class="brush: jscript">
        'A' (Attribute)     -      &lt;span custom-Directive&gt;&lt;/span&gt;
        'C' (Class)         -      &lt;span class="custom-Directive:expression;"&gt;&lt;/span&gt; 
        'E' (Element)       -      &lt;custom-Directive&gt;&lt;/custom-Directive&gt;
        'M' (Comment)       -      &lt;!-- directive: custom-Directive expression --&gt;
        </pre> 
        Also we can use multiple ways to invoke the directive by specifying the following<br />
        restrict: 'EA'
    <br /><br /><br />
    <p><a name="9" style="color: #FF0000">9. Filters in AngularJS
    </a></p>  

    Filters are the view templates available in AngularJS which are used to format data while displaying it to the user. There are some built-in filters provided by AngularJS like as Currency, Date, Number, OrderBy, Lowercase, Uppercase etc. 
    You can also create your own filters.<br />


    <b> Filter Syntax </b>
    {{ expression | filter}}<br /><br />



     <b> Filter Example </b>
     <pre class="brush: jscript">
        <script type="text/javascript">
            {
                {
                    14 | currency
                }
            } 
        </script>

    </pre>


    <br /><br /><br />
    <p><a name="10" style="color: #FF0000">10. Auto bootstrap and Manual bootstrap process in AngularJS
    </a></p>  

    <b>Auto Bootstrap</b><br />
    Basically the initialization of angularjs happens when DOMContentLoaded is triggered or when the whole script is downloaded on client browser with document.readystate is complete. <br />

    After this ng-app gets into action and starts processing all the angularjs components with in the page. It will load all the required modules by the directives and then create application injector and then compile the DOM starting from ng-app root element. This whole process is called auto-bootstrapping.<br /> 

      <pre class="brush: jscript">
        &lt;html&gt;
        &lt;body ng-app="myApp"&gt; 
        &lt;div ng-controller="Ctrl"&gt; Hello {{msg}}! &lt;/div&gt; 

        &lt;script src="lib/angular.js"&gt;&lt;/script&gt; 
        &lt;script&gt;
            var app = angular.module('myApp', []);
            app.controller('Ctrl', function ($scope) {
                $scope.msg = 'World';
            }); 
        &lt;/script&gt; 
        &lt;/body&gt; 
        &lt;/html&gt;
    </pre> 


     <b>Manual Bootstrap</b><br />

     We can manually initialize the bootstrap proecess by invoking angular.bootstrap(). This method takes madules name as input parameters and is called 
     with angular.element(document).ready() which is invoked when DOM is ready. <br />
     ng-app cannot be used while using manual bootstrap process and all modules necessary have to be defined before initializing the process.

     <pre class="brush: jscript">
         &lt;html&gt; 
             &lt;body&gt; 
                 &lt;div ng-controller="Ctrl"&gt; 
                    Hello {{msg}}! 
                 &lt;/div&gt; 
     
                 &lt;script src="lib/angular.js"&gt;&lt;/script&gt; 
                 &lt;script&gt;
                     var app = angular.module('myApp', []);
                     app.controller('Ctrl', function ($scope) {
                         $scope.msg = 'World';
                     }); 
         
                     //manual bootstrap process          
                     angular.element(document).ready(function () {
                         angular.bootstrap(document, ['myApp']);
                     }); 
            
                    &lt;/script&gt;
                &lt;/body&gt; 
            &lt;/html&gt;
     </pre> 


    <br /><br /><br />
    <p><a name="11" style="color: #FF0000">11. Bootstrap multiple modules with auto and manual methods
    </a></p>  
     <pre class="brush: jscript">
    <b>Auto bootstrap multiple modules into single module.</b><br />
     &lt;html&gt; 
         &lt;head&gt; 
         &lt;title&gt;Multiple modules bootstrap&lt;/title&gt; 
             &lt;script src="lib/angular.js"&gt;&lt;/script&gt; 
                 &lt;script&gt;         
                    //module1
                     var app1 = angular.module("module1", []);
                     app1.controller("Controller1", function ($scope) {
                         $scope.name = "Scope1";
                     });

                     //module2 
                     var app2 = angular.module("module2", []);
                     app2.controller("Controller2", function ($scope) {
                         $scope.name = "Scope2";
                     });

                     //module3 dependent on module1 & module2 
                     angular.module("app", ["module1", "module2"]); 
             &lt;/script&gt; 
        &lt;/head&gt;
         
        &lt;body&gt;         
            &lt;!--angularjs auto bootstrap process--&gt; 
            &lt;div ng-app="app"&gt; 
                &lt;h1&gt;Multiple modules bootstrap&lt;/h1&gt; 
                &lt;div ng-controller="Controller2"&gt; {{name}} &lt;/div&gt; 
                &lt;div ng-controller="Controller1"&gt; {{name}} &lt;/div&gt; 
            &lt;/div&gt; 
        &lt;/body&gt; 
    &lt;/html&gt;
    </pre>

    <b>Manually bootstrap your app by using angular.bootstrap() function, for multiple modules.</b><br />
     <pre class="brush: jscript">
    &lt;html&gt;
        &lt;head&gt; 
            &lt;title&gt;Multiple modules bootstrap&lt;/title&gt; 
            &lt;script src="lib/angular.js"&gt;&lt;/script&gt; 
            &lt;script&gt;
                //module1
                var app1 = angular.module("module1", []);
                app1.controller("Controller1", function ($scope) {
                    $scope.name = "Scope1";
                });

                //module2 
                var app2 = angular.module("module2", []);
                app2.controller("Controller2", function ($scope) {
                    $scope.name = "Scope2";
                }); 

                //manual bootstrap process 
                angular.element(document).ready(function () { 
                    var div1 = document.getElementById('div1'); 
                    var div2 = document.getElementById('div2'); 
                    //bootstrap div1 for module1 and module2 
                    angular.bootstrap(div1, ['module1', 'module2']);
                    //bootstrap div2 only for module1 
                    angular.bootstrap(div2, ['module1']);
                }); 
             &lt;/script&gt; 
         &lt;/head&gt; 
        &lt;body&gt; &lt;!--angularjs manual bootstrap process--&gt; 
            &lt;div id="div1"&gt; 
                &lt;h1&gt;Multiple modules bootstrap&lt;/h1&gt; 
                &lt;div ng-controller="Controller1"&gt; {{name}} &lt;/div&gt; 
                &lt;div ng-controller="Controller2"&gt; {{name}} &lt;/div&gt; 
                &lt;/div&gt; 
                &lt;div id="div2"&gt; 
                &lt;div ng-controller="Controller1"&gt; {{name}} &lt;/div&gt; 
                &lt;/div&gt; 
        &lt;/body&gt; 
    &lt;/html&gt;
    </pre>


    <br /><br /><br />
    <p><a name="12" style="color: #FF0000">12. Explain scope in AngularJS
    </a></p>  


    Scope defines the boundary of the execution and evaluation of the expressions, basically it helps in connecting the controller and view. It passes the data between controller and view.
    <br />
    AngularJS has mainly 2 scopes $rootscope and $scope <br />

    $rootscope is the parent of all scopes and an app can have only one rootscope <br />
    
    This is called scope hierarchy. Each controller will have its own $scope which inturn will be a child $rootScope.
    Variables created on $scope within controller will be accessible by the view of that particular controller.


    <pre class="brush: jscript">
        &lt;html&gt; 
        &lt;body ng-app="myApp"&gt; 

            &lt;script src="lib/angular.js"&gt;&lt;/script&gt; 

            &lt;div ng-controller="Ctrl1"&gt; 
            Hello {{msg}}! &lt;br /&gt; 
            Hello {{name}}! (rootScope) 
            &lt;/div&gt; &lt;br /&gt; 
    
            &lt;div ng-controller="Ctrl2"&gt; 
            Hello {{msg}}! &lt;br /&gt; 
            Hey {{myName}}! &lt;br /&gt; 
            Hi {{name}}! (rootScope) 
            &lt;/div&gt; 
    
            &lt;script&gt;
                var app = angular.module('myApp', []);
                app.controller('Ctrl1', function ($scope, $rootScope) {
                    $scope.msg = 'This is from'; $rootScope.name = 'Controller1';
                });

                app.controller('Ctrl2', function ($scope, $rootScope) {
                    $scope.msg = 'This is from Controller2';
                    $scope.myName = $rootScope.name;
                }); 
     
             &lt;/script&gt; 
           &lt;/body&gt; 
        &lt;/html&gt;
    </pre> 

    <b>Scope heirarchy example</b> 

     <pre class="brush: jscript">
    &lt;html&gt; 
    &lt;head&gt; 
    &lt;script src="lib/angular.js"&gt;&lt;/script&gt; 
    &lt;script&gt;
       var app = angular.module('ScopeChain', []); 
       app.controller("parentController", function ($scope) {
           $scope.managerName = 'Manager'; 
           $scope.$parent.companyName = 'Company'; //attached to $rootScope
       });

       app.controller("childController", function ($scope, $controller) {
           $scope.teamLeadName = 'Team Lead';
       }); 
    &lt;/script&gt; 
    &lt;/head&gt;     
        &lt;body ng-app="ScopeChain"&gt; 
            &lt;div ng-controller="parentController"&gt; 
                &lt;table&gt; &lt;%--Parent Controller--%&gt;
                    &lt;tr&gt; &lt;td&gt;Manager Name&lt;/td&gt; &lt;td&gt;{{managerName}}&lt;/td&gt; &lt;/tr&gt; 
                    &lt;tr&gt; &lt;td&gt;Company Name&lt;/td&gt; &lt;td&gt;{{companyName}}&lt;/td&gt; &lt;/tr&gt; 
                        &lt;tr&gt; 
                            &lt;td&gt; 
                                &lt;table ng-controller="childController"&gt; &lt;%--Child Controller--%&gt;
                                &lt;tr&gt; &lt;td&gt;Team Lead Name&lt;/td&gt; &lt;td&gt;{{teamLeadName}}&lt;/td&gt; &lt;/tr&gt; 
                                &lt;tr&gt; &lt;td&gt;Reporting To&lt;/td&gt; &lt;td&gt;{{managerName}}&lt;/td&gt; &lt;/tr&gt; 
                                &lt;tr&gt; &lt;td&gt;Company Name&lt;/td&gt; &lt;td&gt;{{companyName}}&lt;/td&gt; 
                                &lt;/tr&gt; 
                                &lt;/table&gt; 
                            &lt;/td&gt; 
                        &lt;/tr&gt; 
                 &lt;/table&gt;      
             &lt;/div&gt; 
         &lt;/body&gt; 
     &lt;/html&gt;
  </pre> 

    <br />
    <p><b>Difference between $scope and scope </b></p>  

    When dependency injection does not recevie arguments from methods like controller, directive, factory, filter etc then scope object is passed without
    dollar symbol.<br />
    
    Consider Example <br />    
    
    <pre class="brush: jscript">
    module.controller('controller', function ($scope) 
    { 
        //scope sent to controller, this is called dependency injection
    });
    </pre>
    
    <pre class="brush: jscript">
        module.directive('directive', function (scope) 
        { 
            return {       
                   //scope is passed from the calling funtion, this is not dependency injection    
            }; 
        });
    </pre>

    <p><b>Isolate Scope </b> </p>  

    Custom directive can be based on the parent scope as the parent scope can be accessed by the directives.<br />

    <pre class="brush: jscript">
    angular.module('mydirective').directive('sharedScope', function () {
     return { 
     template: 'Name: {{emp.name}} Address: {{emp.address}}' 
     }; 
     });
    </pre>

    Isolate scope doesn’t allow the parent scope to flow down into the directive but shared scope allows the parent scope to flow down into the directive<br />


    Isolate Scope Example <br />
    
    Scope in a directive can be isolated by adding the scope property into the directive.

    <pre class="brush: jscript">
    angular.module('mydirective').directive('sharedScope', function () { 
    return { 
    scope: {}, template: 'Name: {{emp.name}} Address: {{emp.address}}' 
    }; 
    });
    </pre>

    <br /><br /><br />
    <p><a name="13" style="color: #FF0000">13. Compilation process in AngularJS
    </a></p>  

    Angular's compiler is designed to handled new HTML syntax. <br />
    The directives which adds new actions and attributes to HTML controls in AngularJS has been clearly identified by compiler in HTML syntax .<br />
    Basically all compilation is done at the client side itself.<br />
    
    After the HTML page is loaded AngularJS finds all the directives and HTML elements and creates a linking function.<br />
    This linking function produces a view by combing directives with a scope.<br />
    Any changes in the scope model are reflected in the view, and any user interactions with the view are reflected in the scope model.<br />
    
    Normally javascript and jquery produces a string as the end result for HTML view template. But AngularJS directly deal with HTML DOM and updates 
    it as required. It uses two way data binding.<br />


    The $compile traverses the all the HTML elements and scans all directives and makes a list of directives and sorts them based on priority.<br />

    Each directive will be compiled and executed by their own compile function which will update the HTML elements and DOM itself. <br />

    <pre class="brush: jscript">
    var $compile = ...;                     // injected into your code 
    var scope = ...; 
    var parent = ...;                       // DOM element where the compiled template can be appended 
    var html = '<div ng-bind="exp"></div>'; // Step 1: parse HTML into DOM element 
    var template = angular.element(html);   // Step 2: compile the template 
    var linkFn = $compile(template);        // Step 3: link the compiled template with the scope. 
    var element = linkFn(scope);            // Step 4: Append to DOM (optional) parent.appendChild(element);  
    </pre> 

    
    <br /><br />
    <b>
       Compile, Pre and Post linking in AngularJS
    </b>

    <b>Compile</b> – During this phase HTML string is compiled into a template with template function. This function links the scope and template later.
              This compile funtion should be executed prior to template creation and scope linking by AngularJS <br />
    
    
    <b>Post-Link</b> – This should be used only if all the child elements have been compiled and all links of child is executed. All HTML updates and DOM changes
                can be done in this function.   <br />


    <b>Pre-Link</b> – This should be used when all the clild elements are compiled but before the post-link functions of child are executed. Not good practise to 
                make HTML and DOM updates in this face  <br />
                
   
    <pre class="brush :jscript">

        &lt;html&gt; 
            &lt;head&gt; 
                    &lt;script src="lib/angular.js"&gt;&lt;/script&gt; 
                    &lt;script type="text/javascript"&gt;
                        var app = angular.module('app', []);
                        function createDirective(name) {
                            return function () {
                                return {
                                    restrict: 'E', compile: function (htmlElements, attributes) {
                                       //.... compile
                                        return {
                                                    pre: function (scope, htmlElements, attributes) {
                                                            //.... pre link
                                                    },
                                                    post: function (scope, htmlElements, attributes) {
                                                            //.... post link
                                                    }
                                                }
                                    }
                                }
                            }
                        }
                        app.directive('Root', createDirective('Root'));
                        app.directive('Child1', createDirective('Child1'));
                        app.directive('Child2', createDirective('Child2'));
                    &lt;/script&gt; 
            &lt;/head&gt; 
            &lt;body ng-app="app"&gt; 
                &lt;Root&gt; 
                &lt;Child1&gt; 
                &lt;Child2&gt;     
                        This is {{name}} 
                &lt;/Child2&gt; 
                &lt;/Child1&gt; 
                &lt;/Root&gt; 
            &lt;/body&gt; 
        &lt;/html&gt;

      </pre>


       <br /><br /><br />
    <p>
       <a name="14" style="color: #FF0000">14. Templates in AngularJS</a> 
        </p>
    Templates in AngularSJ are nothing but HTML design code which contains AngularJS elements and attributes. These templates display 
    data passed by controller.

    Templates can have Directive, Angular Markup ('{{}}'), Filters and Form Controls of AngularJS <br />

    <pre class="brush :jscript">
        &lt;html ng-app&gt; &lt;!--angular directive--&gt;
            &lt;script src="angular.js"&gt; 
            &lt;body ng-controller="MyController"&gt; 
                &lt;input ng-model="txtName" value="CodeAnalyze"/&gt; 
                  &lt;button ng-click="PostData()"&gt;{{btnText}}&lt;/button&gt;            
           &lt;/body&gt; 
        &lt;/html&gt;
    </pre> 


         <br /><br /><br />
        <p>
       <a name="15" style="color: #FF0000">15. ng-show, ng-hide, ng-if, ng-switch, ng-repeat, ng-include</a> 
        </p>



      <b>ng-show, ng-hide</b><br />

    Used to show and hide HTML controls in AngularJS based on the condition defined in expression.

    <pre class="brush: jscript">
            &lt;div ng-controller="ShowHide"&gt; 
                &lt;div ng-show="data.IsResultTrue"&gt;ng-show Visible&lt;/div&gt; 
                &lt;div ng-hide="data.IsResultFalse"&gt;ng-hide Invisible&lt;/div&gt; 
            &lt;/div&gt;
   

            &lt;script&gt;
                var app = angular.module("app", []);
                app.controller("ShowHide", function ($scope) {
                    $scope.data = {};
                    $scope.data.IsResultTrue = true;
                    $scope.data.IsResultFalse = true;
                });
            &lt;/script&gt;
    </pre>

    <b>ng-if</b><br />
        Used to add and remove HTML controls and elements based on the condition in expression.

     <pre class="brush: jscript">
            &lt;div ng-controller="ngifDemo"&gt; 
                &lt;div ng-if="data.isVisible"&gt;ng-if Visible&lt;/div&gt; 
            &lt;/div&gt; 

            &lt;script&gt;
                 var app = angular.module("app", []);
                 app.controller("ngifDemo", function ($scope) {
                     $scope.data = {};
                     $scope.data.isVisible = true;
                 });
            &lt;/script&gt;
     </pre>

    <b>ng-switch</b><br />

    Used to add and remove HTML controls in AngularJS based on the scope expression.

    <pre class="brush: jscript">
            &lt;div ng-controller="ngswitchDemo"&gt; 
                &lt;div ng-switch on="data.case"&gt; 
                    &lt;div ng-switch-when="1"&gt;Process when case is 1&lt;/div&gt; 
                    &lt;div ng-switch-when="2"&gt;Process when case is 2&lt;/div&gt; 
                    &lt;div ng-switch-default&gt;Process when case is anything else than 1 and 2&lt;/div&gt;
                &lt;/div&gt; 
            &lt;/div&gt; 
        &lt;script&gt;
            var app = angular.module("app", []);
            app.controller("ngswitchDemo", function ($scope)
            {
                $scope.data = {};
                $scope.data.case = true;
            });
        &lt;/script&gt;


    </pre>

    <b>ng-include</b><br />

    Used to add a whole of HTML part usually from external files into the view template.

    <pre class="brush: jscript">
            &lt;div ng-controller="ng-includeDemo"&gt; 
                &lt;div ng-include="'home.html'"&gt;&lt;/div&gt; 
            &lt;/div&gt;
    </pre>


    <b>ng-repeat</b><br />

    Used to loop through the data items or html controls or elements.<br />
    Following are the variables available under ng-repeat which helps in looping thorugh the items.
     $index
     $first
     $middle
     $last

    <pre class="brush: jscript">
            &lt;div ng-controller="ngrepeatDemo"&gt; 
                &lt;ul&gt; 
                    &lt;li ng-repeat="name in names"&gt; {{ name }} &lt;/li&gt; 
                &lt;/ul&gt;
            &lt;/div&gt; 
            &lt;script&gt;                var app = angular.module("app", []);
                app.controller("ngrepeatDemo", function ($scope) {
                    $scope.names = ['Microsft', 'Google', 'Apple'];
                });
            &lt;/script&gt;
        </pre>


    <pre class="brush: jscript">
        &lt;html&gt; 
            &lt;head&gt; 
                &lt;script src="lib/angular.js"&gt;&lt;/script&gt; 
                &lt;script&gt;
                    var app = angular.module("app", []);
                    app.controller("ngrepeatDemo", function ($scope) {
                        $scope.companies = [{ name: 'Microsoft', headquarter: 'Redmond' },
                                          { name: 'Google', headquarter: 'Cupertino' },
                                          { name: 'Apple', headquarter: 'Palo Alto' },
                                          { name: 'Facebook', headquarter: 'Silicon Valley'}];
                    });
                &lt;/script&gt; 
            &lt;/head&gt; 
            &lt;body ng-app="app"&gt; 
                &lt;div ng-controller="ngrepeatDemo"&gt; 
                    &lt;ul&gt; 
                        &lt;li ng-repeat="company in companies"&gt; 
                            &lt;div&gt; 
                                [{{$index + 1}}] {{company.name}} is a {{company.headquarter}}. 
                                &lt;span ng-if="$first"&gt; ... &lt;/span&gt; 
                                &lt;span ng-if="$middle"&gt; ... &lt;/span&gt; 
                                &lt;span ng-if="$last"&gt; ... &lt;/span&gt; 
                            &lt;/div&gt; 
                        &lt;/li&gt; 
                    &lt;/ul&gt; 
                &lt;/div&gt; 
            &lt;/body&gt; 
        &lt;/html&gt;
    </pre>

      <br /><br /><br />
        <p>
       <a name="16" style="color: #FF0000">16. Data Binding in Angular JS</a> 
        </p>

         AngularJS implements two way data binding which synchronizes the data between model and view. With this developers need not bother about
    updating the UI based on the model changes. <br />
    
    Angular uses $watch APIs to observe model changes on the scope. Angular registered watchers for each variable
    on scope to observe the change in its value. If the value of variable on scope is changed then the view gets updated automatically.
    This automatic change happens because of $digest cycle is triggered. <br />
    So Angular processes all registered watchers on the current scope and its 
    children and checks for model changes and calls dedicated watch listeners until the model is stabilized and no more listeners are fired. <br />
    Once the $digest loop finishes the execution the browser re-renders the DOM and reflects the changes. <br />
    
    If some variables are not needed to be monitered and increase the speed and efficency then One way binding can be used.

    AngularJS  monitores the variables and implements data binding with the help of following 3 functions: $watch(), $digest() and $apply(). 

    <br /><b>Two-way data binding</b>  - It is used to synchronize the data between model and view. It means, any change in model will update 
    the view and vice versa. ng-model directive is used for two-way data binding. <br />

    <br /><b>One-way data binding</b>  - From AngularJS 1.3 an expression that starts with double colon (::) is said to be one-time expression which is one-way binding. <br />
        <pre class="brush: jscript">
    &lt;div ng-controller="DataBinding"&gt; 
        &lt;label&gt;Name (two-way binding): &lt;input type="text" ng-model="company" /&gt;&lt;/label&gt; 
        &lt;strong&gt;Your name (one-way binding):&lt;/strong&gt; {{::company}}&lt;br /&gt; 
        &lt;strong&gt;Your name (normal binding):&lt;/strong&gt; {{company}} 
    &lt;/div&gt; 
    &lt;script&gt;
        var app = angular.module('app', []);
        app.controller("DataBinding", function ($scope) {
            $scope.company = "Apple"
        }) 
    &lt;/script&gt;

    </pre> 


      <br /><br /><br />
        <p>
       <a name="17" style="color: #FF0000">17. Explain $watch, $digest and $apply in AngularJS?</a> 
        </p>
    

    <b>$watch()</b><br />
    This function is used to observe changes in a variable on the $scope. It accepts three parameters: expression, listener and equality object, 
    where listener and equality object are optional parameters. $watch(watchExpression, listener, [objectEquality])


    <pre class="brush :jscript">
    &lt;html&gt; 
        &lt;head&gt; 
            &lt;script src="lib/angular.js"&gt;&lt;/script&gt; 
            &lt;script&gt; 
                var myapp = angular.module("AngularJSWatchApp", []); 
                var myController = myapp.controller("WatchDemo", function ($scope) {
                    $scope.name = 'dotnet-tricks.com'; 
                    $scope.counter = 0; 
                    $scope.$watch('name', function (newValue, oldValue) { 
                        $scope.counter = $scope.counter + 1; 
                    }); 
                }); 
            &lt;/script&gt; 
        &lt;/head&gt; 
        &lt;body ng-app="myapp" ng-controller="WatchDemo"&gt; 
            &lt;input type="text" ng-model="name" /&gt; &lt;br /&gt;&lt;br /&gt; 
            Counter: {{counter}} 
        &lt;/body&gt; 
    &lt;/html&gt;
    </pre>


     <b>$digest()</b><br />
      Loops through all watches in the $scope and call the listener with new value and old value if the value of the expression is changed. This is invoked by
    AngularJS itself and we can manually also invoke this.
                    
        <pre class="brush :jscript">
        &lt;body ng-app="app"&gt; 
            &lt;div ng-controller="DigestDemo"&gt; 
                &lt;button class="digest"&gt;Scope Digest&lt;/button&gt; &lt;br /&gt; 
                &lt;h2&gt;obj value : {{obj.value}}&lt;/h2&gt; 
            &lt;/div&gt; 
            &lt;script&gt; 
                var app = angular.module('app', []); 
                app.controller('DigestDemo', function ($scope) {
                    $scope.obj = { value: 1 }; 
                    $('.digest').click(function () {                        
                        console.log($scope.obj.value++); 
                        $scope.$digest();
                    }); 
                }); 
            &lt;/script&gt; 
        &lt;/body&gt; 
</pre> 

     <b>$apply()</b><br />
        To update the model not inside the context of the AngularJS we need to call $apply(). After this
        $digest() is called internally to keep data bindings updated.
    
        <pre class="brush :jscript">
    &lt;html&gt; 
        &lt;head&gt;             
            &lt;script src="lib/angular.js"&gt;&lt;/script&gt; 
            &lt;script&gt; 
                var myapp = angular.module("myapp", []); 
                var myController = myapp.controller("ApplyDemo", function ($scope) { 
                    $scope.datetime = new Date(); 
                    $scope.updateTime = function () { 
                        $scope.datetime = new Date(); 
                    } //outside angular context 
                    document.getElementById("updateTimeButton").addEventListener('click', function () { //update the value 
                        $scope.$apply(function () {
                            $scope.datetime = new Date();
                            console.log($scope.datetime);
                        });
                    });
                });
            &lt;/script&gt; 
        &lt;/head&gt; 
        &lt;body ng-app="myapp" ng-controller="ApplyDemo"&gt; 
            &lt;button ng-click="updateTime()"&gt;Update time - ng-click&lt;/button&gt; 
            &lt;button id="updateTimeButton"&gt;Update time&lt;/button&gt; &lt;br /&gt; {{datetime | date:'yyyy-MM-dd HH:mm:ss'}} 
        &lt;/body&gt; 
    &lt;/html&gt;
</pre> 

    So the difference between $apply() and $digest() is apply handles the entire scope with parent and all child but digest handles only current scope with
    all parent and child which make digest() faster than apply.<br />


    $apply() uses $exceptionHandler service to handle errors where as we will have to handle errors in $digest(). <br />
    
        <pre class="brush :jscript">
    function $apply(expr) 
    { 
        try 
        { 
            return $eval(expr); 
        } 
        catch (e) 
        {
            $exceptionHandler(e); 
        } 
        finally 
        { 
            $root.$digest(); 
        } 
    }
</pre> 


       <br /><br /><br />
       <p>
       <a name="18" style="color: #FF0000">18. Explain $watch(), $watchgroup() and $watchCollection() in AngularJS?</a> 
        </p>

        <b>$watch</b><br />
        Monitors the changes in variable on the $scope. It has 3 parameters: expression, listener and equality object
        where listener and equality object are optional parameters. 
    
        $watch(watchExpression, listener, [objectEquality])


        watchExpression - is the expression in the scope to watch. This expression is called on every $digest() and returns the value that is being watched.<br />
        listener - is optional parameter which is the function called when watchExpression value changes else it won't be called.<br />
        objectEquality - is the optional boolean parameter used for comparing the objects for equality using angular.equals instead of comparing for 
        reference equality. <br />
    
        scope.name = 'abcd'; 
        scope.counter = 0; 
        scope.$watch('name', function (newVal, oldVal) { 
            scope.counter = scope.counter + 1; 
        });


        <b>$watchgroup</b><br />
        This is same as $watch but only difference here is it handles group of elements, watchExpression and listener are passed in the form of arrays.<br />

        <pre class="brush :jscript">
        $scope.product = ''; 
        $scope.company = ''; 
        $scope.$watchGroup(['product', 'company'], function(newVal, oldVal) {
                if(newVal[0]  = 'Windows')
                { 
                    $scope.result = 'Microsoft'; 
                } 
                else if (newVal[1] = 'Android')
                {   
                    $scope.result = 'Google'; 
                });

        </pre> 

        <b>$watchCollection</b> <br />
        This is used to monitor an object, when the properties of the object changes then the listener is invoked. This atakes obj and listener as parameters.
        $watchCollection(obj, listener)
              
        <pre class="brush :jscript">      
        $scope.names = ['Microsoft', 'Google', 'Apple']; 
        $scope.dataCount = 3; 
        $scope.$watchCollection('names', function (newVal, oldVal){ 
        $scope.dataCount = newVal.length; 
        });
        </pre> 



    <%-- <br /><br /><br />
    <p><b style="font-weight: bold; color: #FF0000">
       19. AngularJS scope life-cycle
    </b></p>

   
1. Creation – This phase initialized the scope. The root scope is created by the $injector when the application is bootstrapped. During template linking, some directives create new child scopes.
A digest loop is also created in this phase that interacts with the browser event loop. This digest loop is responsible to update DOM elements with the changes made to the model as well as executing any registered watcher functions.
2. Watcher registration - This phase registers watches for values on the scope that are represented in the template. These watches propagate model changes automatically to the DOM elements.
You can also register your own watch functions on a scope value by using the $watch() function.
3. Model mutation - This phase occurs when data in the scope changes. When you make the changes in your angular app code, the scope function $apply() updates the model and calls the $digest() function to update the DOM elements and registered watches.
When you do the changes to scope inside your angular code like within controllers or services, angular internally call $apply() function for you. But when you do the changes to the scope outside the angular code, you have to call $apply() function explicitly on the scope to force the model and DOM to be updated correctly.
4. Mutation observation – This phase occurs when the $digest() function is executed by the digest loop at the end of $apply() call. When $digest() function executes, it evaluates all watches for model changes. If a value has been changed, $digest() calls the $watch listener and updates the DOM elements.
5. Scope destruction – This phase occurs when child scopes are no longer needed and these scopes are removed from the browser’s memory by using $destroy() function. It is the responsibility of the child scope creator to destroy them via scope.$destroy() API.
This stop propagation of $digest calls into the child scopes and allow the memory to be reclaimed by the browser garbage collector.


Q60. Explain digest life-cycle in AngularJS?
Ans. The digest loop is responsible to update DOM elements with the changes made to the model as well as executing any registered watcher functions.
The $digest loop is fired when the browser receives an event that can be managed by the angular context. This loop is made up of two smaller loops. One processes the $evalAsync queue and the other one processes the $watch list.
The $digest loop keeps iterating until the $evalAsync queue is empty and the $watch list does not detect any changes in the model.
The $evalAsync queue contains those tasks which are scheduled by $evalAsync() function from a directive or controller.
The $watch list contains watches correspondence to each DOM element which is bound to the $scope object. These watches are resolved in the $digest loop through a process called dirty checking. The dirty checking is a process which checks whether a value has changed, if the value has changed, it set the $scope as dirty. If the $scope is dirty, another $digest loop is triggered.
When you do the changes to the scope outside the angular context, you have to call $apply() function explicitly on the scope to trigger $digest cycle immediately.


--%>



    </div>
</asp:Content>

