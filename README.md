# TransformerTextToMusic

TransformerTextToMusic is an open-source console application written in C# that converts Ukrainian text into music.

The program analyzes Ukrainian text, transforms letters and letter combinations into configurable music tokens, and then converts those tokens into musical notes played in real time using sampled instruments.

This project was created as an experiment in procedural music generation and as a tool for producing unique melodies without relying on external music libraries, composers, or copyrighted musical assets.

## Features

* Ukrainian text to music conversion
* Support for both single letters and multi-letter phonetic combinations (for example: "дз", "дж", "дзь", "нь", "сь")
* Editable token mapping
* Real-time playback
* Multiple instrument support
* Polyphonic playback mode
* User-replaceable instrument samples
* Open-source under the MIT License

## How It Works

The conversion process consists of two stages:

1. Ukrainian text is transformed into a sequence of tokens.
2. Tokens are transformed into musical notes and played in real time.

Unlike traditional music generators, the program does not use pre-written melodies, machine learning models, or external musical databases.
The resulting melody is generated entirely from the input text and the configured token-to-note mapping.

## Instruments

The project includes a collection of instrument samples stored in the `Instruments` folder.

Users may replace these samples with their own recordings or higher-quality alternatives.
Since tokens are mapped to note ranges rather than specific recordings, custom sample replacement can be performed without changing the core conversion logic.

## Configuration

Most conversion parameters can be modified through external text files without recompiling the application.

This includes:

* text input
* token mappings
* playback settings
* instrument assignments

## Project Goals

The primary goal of this project was to explore procedural music generation based on language structure.

A secondary goal was to create a self-contained source of musical content for personal game development projects, reducing dependence on third-party assets and minimizing potential copyright concerns associated with using pre-existing music.

## Technical Information

* Language: C#
* Framework: .NET 9
* Audio Library: NAudio
* Interface: Console Application

## Project Status

This repository contains the final version of TransformerTextToMusic.

Future development, if any, will be performed as a separate next-generation project built on a different framework and architecture.
