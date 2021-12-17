## Best Practices in ASP.NET Core: Entities, Validation, & View Models
- by Shawn Wildermuth

- THE BIG PICTURE:
    - Seperate entities & persistence. Make sense of the role of View Models. Introduce Validation.
    - Visual Studio. .NET Core 3.x. EF Core.
    - Entities are nouns. Person. Invoice. Log.
    - Time billing system. A lawyer has clients. Clients have addresses and cases. A timebill encapusalutes cases and employees and it generates invoices.
    - Entities have a primary key. And a foreign key. And can have a natural key. e.g.: social security number.
    - Attribute types:
        - Simple: Single scalar types. e.g.: birthday, name, isRegistered.
        - Composite: Complex types owned by the entity. e.g.: Address.
        - Collections: Multi-valued attributes. e.g. Phone numbers.
    - DEMO: The project.
    - Entities in the abstract sense. Not with respect to EF.
        - Entities are types of data structures that are persisted between invocations.
        - Persistence: SQL/EF Core.
    - What we've learned:
        - Entities are persistable data structures that are needed in most applications.
        - Think about entities as a single unit of work instead of 'tables.'
        - It's not longer just SQL. Different data needs different stores.

- WHY VIEWMODELS MATTER:
    - A use-case -specific structure of data for binding to physical views, logical views, ar APIs.
    - NOTE: In ASP.NET Core any data that is passed is considered a model in ModelBinding, even if it is your entities.
    - Why? (1) Customized use cases. (2) Limiting sensitive data. (3) Streamlining binding. (4) More complex validation.
    - Pragmatism. Are not always required. Data can be exposed as entities. 1:1 relationship can be viewed as an anti-pattern. 
    - Unnecessary complexity. Can loosly-type if schedule required.
    - NOTE: This will 'map' in AutoMapper:
    ```csharp
        public string AddressAddress1 { get; set; }
    ```
    - Create a ViewModel for every entity you have.
    - SUMMARY:
        - ViewModels can allow us to simply the code in our views and forms.
        - Mapping can simplify using ViewModels in our code.
        - Mapping is a commodity.

- SERVER-SIDE VALIDATION:
    - Importance:
        - Ensuring that a program operates on clean, correct, and useful data. Check for correctness, meanigfulness, and security of input data.
        - Data validation is no substitute for real business rules.
        - Database schema is about storage requirements. Validation is about correctness.
    - Using validation attributes in models:
    - Using fluent validation in models:
    - Use validation in forms:
    - Explain model validation:
        ```csharp
            ModelState.AddModelError(k, v);
        ```
    - Asynchronous validation:
    - What we've learned:
        - Validation can be encapsulated in Attributes or FLuentValidation.
        - Keep all validation in the validation system. Not in the controller.
        - You can perform rich. asynchronous validation on the server-side.

- MODELS IN APIS:
    - Why? Using VMs within API controllers.
        - Data shape is the contract in API. Underlying changes to DB may not affect.
        - VMs can be tied to APIs. Validation is required for the server.
    - And validation.
    - What we've learned:
        - View Models are just as simple in API as in Web controllers.
        - Mapping assists copy changes to existing datastore objects.
        - Validation is key as a gate keeper.

- CLIENT-SIDE MODEL:
    - What are client-side VMs for?
        - Helpful inTypeScript scenarios. Could use JavaScript, but does not guarentee anything.
        - Client-side validation is for UX. Not for security.
    - Using FluentValidation client-side: Only generates ASP.NET Validation.
        - data-val- attributes play well with the included j-query libraries.
    - Validation in ng.
        ```javascript
            npm install
            npm install @angular/cli -g
            npm run watch
        ```
        - Angular route: /timesheet
        ```javascript
            save() {
            let timeBill: ITimeBillViewModel = this.theForm.value;
        ```
    - What we've learned:
        - Validation on the client is not magically secure.
        - Often you'll need to duplicate validation from the server.
        - View models are useful but noy a panacea in either JavaScript or TypeScript.