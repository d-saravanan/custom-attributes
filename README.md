# Attribute based logic implementation for Business Models

This application demonstrates the automated processing of the attributes with the business logic. The models are decorated with the attributes and the functionality is implemented by the attributes, facilitating the clear separation of concerns and the ease of use

**Example**

Consider the following example of a model class named person
<pre>
[Serializable]
public class Person
{
    public string Id { get; set; }
    [JsonContent("FirstName,LastName,UserName", 0), StringSecureContent(1)]
    public string Profile { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string EmailId { get; set; }
    public string Phone { get; set; }
}
</pre>

In some business use-cases, the data may be stored in the compressed json format for some of the fields and sent to the datastore [Database, typically].
During the retrieval from the datastore, they may be expanded back to the original state.

In other cases, some sensitive information like the user key / token etc may be stored in the encrypted format and then after retrieval, they may need to be decrypted to verify some values.
The above logic is **NOT** applicable for the Passwords as they are best hashed and stored.

In such cases, the developer has to work on writing the serialization / encryption logic in the services in which case, I feel that the service has been burdened with the knowledge of the encryption / serialization

In order to avoid these cases, this application presents a clear separation of concerns that enables the use of attributes. 

**Steps**
```
1. Select the property whose values needs to be secured,
2. Mark that property with the SecuredContent attribute
3. Implement the IDataProtector and create a protector that can take care of protecting and unprotecting the data as per your security demands
4. In the secured attribute, for the Bind() method invokes the IDataProtector and calls the protect method which protects the data and then the protected data is replaced with the original ones
```

Similar is the case for Serialization.
In case of the JsonContent attribute, the Serializer is pluggable, hence the right choice of the serialization can be set as per the demands.

This is a completely flexible approach with all the dependencies referred as interfaces

In case of writing a new attribute that can be used to implement similar to the ones described above, kindly refer the 'JsonContent.cs' file

**Steps**
```
1. Create a class that derives from the 'DataPropertyAttribute' base class
2. Decroate this class with the corresponding AttributeTarget attribute
3. Implement the constructors with the required paramters
4. override the base Bind & UnBind methods and implement the business logic
5. In the target class [Model Class], use the attribute on the Class / Property etc as per the (2) attribute targets mentioned.
```

In case of any issues / help / suggestions / feedback / contacting me, please leave a message here or to my email id [s.dorai2009@gmail.com]
