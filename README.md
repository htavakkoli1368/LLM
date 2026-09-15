# LLM & RAG with C# and Groq

A hands-on C# project for learning how **Large Language Models (LLMs)** and **Retrieval-Augmented Generation (RAG)** work in practice.

This repository starts with a simple LLM integration using **Groq**, then builds a basic RAG pipeline that retrieves information from a local document before sending it to the LLM.

The goal is not to build a production-ready RAG system yet. The goal is to understand the concepts step by step and see how the pieces fit together.

---

## 🚀 What is inside?

This solution currently contains two console applications:

### 1. Simple LLM

A minimal example of communicating with an LLM using the Groq API.

The application:

1. Takes a question
2. Sends it to an LLM
3. Receives the response
4. Prints the answer

Conceptually:

```text
User Question
      ↓
   Groq API
      ↓
      LLM
      ↓
    Answer
```

This is the starting point for understanding how an application can communicate with an LLM.

---

### 2. Simple RAG

The second console application demonstrates a very basic **Retrieval-Augmented Generation** pipeline.

Instead of asking the LLM to answer using only its existing knowledge, the application reads information from a local document and retrieves relevant content before sending the request to the model.

The current retrieval strategy is intentionally simple and **keyword-based**.

```text
Document
   ↓
Read Document
   ↓
Keyword Retrieval
   ↓
Relevant Context
   ↓
LLM
   ↓
Answer
```

For example, given a company policy document:

```text
Employees receive 20 days of annual leave per year.
```

The application can retrieve the relevant information and provide it to the LLM together with the user's question.

---

## 🧠 Why RAG?

LLMs have a lot of knowledge, but they don't automatically have access to your private or external data.

For example:

* Company policies
* Internal documentation
* Product manuals
* Private knowledge bases
* Customer data
* Frequently changing information

Instead of retraining the model every time this information changes, RAG allows an application to retrieve relevant information at **inference time** and provide it to the model as context.

The basic idea is:

```text
User Question
      ↓
Retrieve Relevant Information
      ↓
Augment the Prompt with Context
      ↓
Generate Answer with LLM
```

That's where the name comes from:

**R**etrieval
**A**ugmented
**G**eneration

---

## 🏗️ Project Structure

```text
.
├── SimpleLLM/
│   └── Program.cs
│
├── SimpleRAG/
│   ├── Program.cs
│   └── company-policy.txt
│
└── README.md
```

> The exact project names may vary depending on the solution structure.

---

## 🔑 Requirements

Before running the applications, you need:

* .NET SDK
* A Groq API key
* An environment variable named `GROQ_API_KEY`

You can create a Groq API key from the Groq Console.

Do not hard-code your API key inside the source code.

---

## ⚙️ Configuration

Set the following environment variable:

```text
GROQ_API_KEY=your-api-key
```

The applications read the key from the environment:

```csharp
string apiKey =
    Environment.GetEnvironmentVariable("GROQ_API_KEY")
    ?? throw new Exception("GROQ_API_KEY is not set");
```

---

## ▶️ Running the project

Clone the repository:

```bash
git clone <your-repository-url>
```

Move into the project directory:

```bash
cd <your-project-directory>
```

Make sure `GROQ_API_KEY` is configured and then run the desired console application.

For example:

```bash
dotnet run
```

---

## 🔍 How the Simple RAG Works

The current RAG implementation intentionally keeps the retrieval layer simple.

### Step 1: Read the document

The application loads the local document:

```csharp
string document =
    await File.ReadAllTextAsync("company-policy.txt");
```

### Step 2: Receive the question

For example:

```text
How many annual leave days can I take?
```

### Step 3: Retrieve relevant information

The application searches the document using keywords from the question.

```text
Question
   ↓
Extract Keywords
   ↓
Search Document
   ↓
Relevant Lines
```

### Step 4: Build the context

The retrieved information becomes the context sent to the LLM.

```text
Context:
Employees receive 20 days of annual leave per year.

Question:
How many annual leave days can I take?
```

### Step 5: Generate the answer

The LLM receives both the context and the question and generates the final response.

---

## ⚠️ Current Limitations

This is intentionally a **learning implementation**, not a production RAG system.

The current retrieval mechanism is keyword-based, which means it can struggle with semantically similar questions that use different words.

For example:

```text
Document:
Employees receive 20 days of annual leave per year.

Question:
What is my yearly vacation allowance?
```

The meaning is similar, but the keywords are different.

A keyword-based retriever may fail to identify the correct document section.

This limitation leads to the next step in the project:

**Semantic Search.**

---

## 🧭 What's Next?

The project is being developed step by step.

### Current

```text
LLM API
   ↓
Simple RAG
   ↓
Keyword Retrieval
```

### Next

```text
Document
   ↓
Chunking
   ↓
Embeddings
   ↓
Vector Search
   ↓
Relevant Context
   ↓
LLM
```

Then:

```text
Embeddings
     ↓
Vector Database
     ↓
Semantic Search
     ↓
RAG
     ↓
ASP.NET Core API
```

The goal is to gradually move from a simple keyword-based RAG implementation toward a more realistic production-oriented architecture.

---

## 🎓 Learning Path

This repository is part of a practical learning journey around LLMs and RAG with C#.

The concepts covered so far include:

* Tokens
* Token IDs
* Embeddings
* Embedding Lookup
* Vectors
* Transformers
* LLM APIs
* Retrieval-Augmented Generation
* Keyword-based Retrieval

Upcoming topics:

* Semantic Search
* Embeddings in RAG
* Vector Similarity
* Vector Databases
* RAG with ASP.NET Core
* More advanced AI application patterns

---

## 🛠️ Tech Stack

* **C#**
* **.NET**
* **Groq API**
* **LLM**
* **Retrieval-Augmented Generation (RAG)**

---

## 📌 Why this repository?

This project is intentionally built incrementally.

Instead of jumping directly into frameworks and complex RAG architectures, each step focuses on understanding what is happening underneath.

The idea is simple:

> **Understand the concept first. Then add the abstraction.**

---

## 📚 Related Content

I am also documenting this learning journey through practical technical content, covering LLMs, embeddings, RAG, vector search, and AI application development with C#.

If you're interested in building AI-powered applications with **.NET and C#**, follow along as the project evolves.

---

## ⭐ Feedback & Contributions

This repository is primarily a learning project, so feedback, ideas, and improvements are welcome.

If you find it useful, consider giving the repository a ⭐.
