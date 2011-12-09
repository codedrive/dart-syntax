// Copyright (c) 2011, the Dart project authors.  Please see the AUTHORS file
// for details. All rights reserved. Use of this source code is governed by a
// BSD-style license that can be found in the LICENSE file.
// http://dart.googlecode.com/svn/trunk/dart/language/grammar/Dart.g

grammar Dart;

options {
    language=CSharp2;
}

tokens {
  BREAK      = 'break';
  CASE       = 'case';
  CATCH      = 'catch';
  CONST      = 'const';
  CONTINUE   = 'continue';
  DEFAULT    = 'default';
  DO         = 'do';
  ELSE       = 'else';
  FALSE      = 'false';
  FINAL      = 'final';
  FINALLY    = 'finally';
  FOR        = 'for';
  IF         = 'if';
  IN         = 'in';
  NEW        = 'new';
  NULL       = 'null';
  RETURN     = 'return';
  SUPER      = 'super';
  SWITCH     = 'switch';
  THIS       = 'this';
  THROW      = 'throw';
  TRUE       = 'true';
  TRY        = 'try';
  VAR        = 'var';
  VOID       = 'void';
  WHILE      = 'while';

  // Pseudo-keywords that should also be valid identifiers.
  ABSTRACT   = 'abstract';
  ASSERT     = 'assert';
  CLASS      = 'class';
  EXTENDS    = 'extends';
  FACTORY    = 'factory';
  GET        = 'get';
  IMPLEMENTS = 'implements';
  IMPORT     = 'import';
  INTERFACE  = 'interface';
  IS         = 'is';
  LIBRARY    = 'library';
  NATIVE     = 'native';
  NEGATE     = 'negate';
  OPERATOR   = 'operator';
  SET        = 'set';
  SOURCE     = 'source';
  STATIC     = 'static';
  TYPEDEF    = 'typedef';
}

// -----------------------------------------------------------------
// Lexical tokens.
// -----------------------------------------------------------------
IDENTIFIER_NO_DOLLAR
    : IDENTIFIER_START_NO_DOLLAR IDENTIFIER_PART_NO_DOLLAR*
    ;

IDENTIFIER
    : IDENTIFIER_START IDENTIFIER_PART*
    ;

HEX_NUMBER
    : '0x' HEX_DIGIT+
    | '0X' HEX_DIGIT+
    ;

NUMBER
    : DIGIT+ NUMBER_OPT_FRACTIONAL_PART EXPONENT? NUMBER_OPT_ILLEGAL_END
    | '.' DIGIT+ EXPONENT? NUMBER_OPT_ILLEGAL_END
    ;

fragment NUMBER_OPT_FRACTIONAL_PART
    : ('.' DIGIT)=> ('.' DIGIT+)
    | // Empty fractional part.
    ;

fragment NUMBER_OPT_ILLEGAL_END
    : (IDENTIFIER_START)=> { error("numbers cannot contain identifiers"); }
    | // Empty illegal end (good!).
    ;

fragment HEX_DIGIT
    : 'a'..'f'
    | 'A'..'F'
    | DIGIT
    ;

fragment IDENTIFIER_START
    : IDENTIFIER_START_NO_DOLLAR
    | '$'
    ;

fragment IDENTIFIER_START_NO_DOLLAR
    : LETTER
    | '_'
    ;

fragment IDENTIFIER_PART_NO_DOLLAR
    : IDENTIFIER_START_NO_DOLLAR
    | DIGIT
    ;

fragment IDENTIFIER_PART
    : IDENTIFIER_START
    | DIGIT
    ;

// Bug 5408613: Should be Unicode characters.
fragment LETTER
    : 'a'..'z'
    | 'A'..'Z'
    ;

fragment DIGIT
    : '0'..'9'
    ;

fragment EXPONENT
    : ('e' | 'E') ('+' | '-')? DIGIT+
    ;

STRING
    : '@'? MULTI_LINE_STRING
    | SINGLE_LINE_STRING
    ;

fragment MULTI_LINE_STRING
options { greedy=false; }
    : '"""' .* '"""'
    | '\'\'\'' .* '\'\'\''
    ;

fragment SINGLE_LINE_STRING
    : '"' STRING_CONTENT_DQ* '"'
    | '\'' STRING_CONTENT_SQ* '\''
    | '@' '\'' (~( '\'' | NEWLINE ))* '\''
    | '@' '"' (~( '"' | NEWLINE ))* '"'
    ;

fragment STRING_CONTENT_DQ
    : ~( '\\' | '"' | NEWLINE )
    | '\\' ~( NEWLINE )
    ;

fragment STRING_CONTENT_SQ
    : ~( '\\' | '\'' | NEWLINE )
    | '\\' ~( NEWLINE )
    ;

fragment NEWLINE
    : '\n'
    | '\r'
    ;

BAD_STRING
    : UNTERMINATED_STRING NEWLINE { error("unterminated string"); }
    ;

fragment UNTERMINATED_STRING
    : '@'? '\'' (~( '\'' | NEWLINE ))*
    | '@'? '"' (~( '"' | NEWLINE ))*
    ;

HASHBANG
    : '#!' ~(NEWLINE)* (NEWLINE)?
    ;

// -----------------------------------------------------------------
// Whitespace and comments.
// -----------------------------------------------------------------
WHITESPACE
    : ('\t' | ' ' | NEWLINE)+ { $channel=HIDDEN; }
    ;

SINGLE_LINE_COMMENT
    : '//' ~(NEWLINE)* (NEWLINE)? { $channel=HIDDEN; }
    ;

MULTI_LINE_COMMENT
    : '/*' (options { greedy=false; } : .)* '*/' { $channel=HIDDEN; }
    ;