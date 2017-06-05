OCAS Coltrane XML Helper Library
================================

[![NuGet](https://img.shields.io/nuget/v/ocas.coltrane.collegetransmission.1.0.0.svg)](https://www.nuget.org/packages?q=Tags%3A%22OCAS%22)

Table of Contents
-----------------
* [Overview](#overview)
* [Coltrane Standard](#coltrane-standard)
* [Getting Started](#getting-started)
* [Sample](#sample)
* [XML](#xml)
* [JSON (sorta)](#json-sorta-)
* [Requirements](#requirements)

Overview
--------

This library helps serialize and deserialize to and from the OCAS Coltrane
standard. One common mistake is to construct an xml file with the same elements
and attributes and then assume you are following the xchema. This is incorrect
and you need the proper namespaces and validation to truely ensure that your file
is compliant with the schema. This library sets out to help solve this.

Coltrane Standard
-----------------

While the library can help you construct valid xml (against the schema), it cannot
guarantee that you follow all of the recommendations in the Coltrane implementation guides.
Those guides should be followed when using this helper library.

Getting Started
---------------

The package(s) are available on [nuget.org](https://www.nuget.org/packages/Ocas.Coltrane.CollegeTransmission.1.0.0/).

You will notice that the compiled Assemvly Version is different than the Friendly Name Version. This is because the Friendly Name has the XML Schema version included in the title (as we support backwards compatibility).

### 2 Minute Tutorial ###

Once the source is added, open your project and install the package you want.

`nuget install-package Ocas.Coltrane.CollegeTransmission.1.0.0`

Now you can use the library to **serialize**:

```csharp
using Ocas.Coltrane;
...
var app = new CollegeTransmissionType
{
    Header = new HeaderType(),
    Trailer = new TrailerType()
};

string xml = null;
app.Serialize(out xml);
// Or use Serialize and validate, although this would fail.
app.SerializeAndValidate(out xml);
```

or **deserialize**:

```csharp
using Ocas.Coltrane;
...
var xmlString = System.IO.File.ReadAllText("my.xml");
var app = AdmissionsApplication.Deserialize(xmlString);
```

---

Sample
------

We've provided a sample solution in this github repository that you can download and
run locally. It uses some dummy sample Coltrane XML files.

---

XML
---

These helpers are the bread and butter of this library. It helps abstract away the
duties of serialization and deserialization of Coltrane XML.

### XML Serialization ###

**_obj_.Serialize(...)** - The bread and butter of serialization. This
will convert your object into Coltrane compliant XML (with proper namespaces).

**_obj_.SerializeAndValidate(...)** - This will check to make sure the
xml generated passes schema validation. The schemas are embedded resources within
the dll. The unit tests have an example of the minimum CollegeTransmission object
required to pass schema validation.

### XML Save To File ###

**_obj_.SaveToFile(...)** - Converts the Generated object to XML and saves to a file.

### XML Deserialization ###

**_ElementType_.DeserializeUnrestricted(...)** - This deserialize method will ignore
the presence of namespaces, which will be important if you don't want to update your
code if there is a new version of the XML (without any breaking changes).

**_ElementType_.Deserialize(...)** - This will take the input string/stream and
construct a Coltrane object. It will fail if the proper namespaces aren't present.

**_ElementType_.DeserializeAndValidate(...)** - This will make sure that the xml
is compliant with the Coltrane schema.

### XML LoadFromFile ###

**_ElementType_.LoadFromFileUnrestricted(...)** - This method will read from a file and
then calls the _DeserializeUnrestricted(...)_ from the previous section.

**_ElementType_.LoadFromFile(...)** - This will read from the file and then call
_Deserialize(...)_ from the previous section.

**_ElementType_.LoadFromFileAndValidate(...)** - This will read from the file and then call _DeserializeAndValidate(...)_ from the previous section.

JSON (sorta)
------------

This is where things get difficult. Because to truely serialize and deserialize
following a schema, you obviously need a [JSON schema](http://json-schema.org/).
Unfortunately Coltrane only defines an XML schema, and so naturally we cannot validate
against a JSON object. But we can still provide the ability to serialize to JSON
from our valid XML.

### JSON Serialization ###

This is possible, but requires an extra step by using the XML Schema to validate,
then conversion into JSON.

ElementType -> Serialize to XML -> Validate XML -> Load XML into XmlDocument ->
Serialize with Newtonsoft.

This isn't ideal, but must be done this way to:

  1. Get the validation from the schema
  1. Not get the Item(s) and ItemElement(s) from the generated classes (see above
  Choice Elements blurb)

Here's the code snippit that will enable generation of a JSON string (requires
Newtonsoft JSON).

```csharp
public class ColtraneConverter : JsonConverter
{
    public override bool CanRead { get { return false; } }

    public override bool CanConvert(Type objectType)
    {
        var types = new[] { typeof(AdmissionsApplication) };
        return types.Any(t => t == objectType);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object
    existingValue, JsonSerializer serializer)
    {
        throw new NotImplementedException("Unnecessary because CanRead is false.
        The type will skip the converter.");
    }

    public override void WriteJson(JsonWriter writer, object value, JsonSerializer
    serializer)
    {
        var obj = (CollegeTransmissionType)value;

        string objXml = null;

        obj.SerializeAndValidate(out objXml);

        var xDoc = XDocument.Parse(objXml);
        var xml = new XmlDocument();

        xml.LoadXml(objXml);

        serializer.Serialize(writer, xDoc);
    }
}
```

### JSON Deserialization ###

Not possible right now. This is mainly because of the Choice Elements in the XML
schema mentioned in the XML section. It might be possible to use the [XmlSchemaClassGenerator](https://github.com/mganss/XmlSchemaClassGenerator)
because in [their doc](https://github.com/mganss/XmlSchemaClassGenerator#choice-elements),
they explain that they explicitally treat _Choice Elements_ as _Sequences_. I suspect
that this could be used to generate the classes, but as they say, _the user will
have to take care to only set a schema valid combination_. Something to explore
if JSON Deserialization ever becomes required.

Requirements
------------

.NET 4.0