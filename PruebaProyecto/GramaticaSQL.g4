grammar GramaticaSQL;

/*
 * Parser Rules
 */

consulta: SELECT atributo ESP from tabla postInfo fin ;

atributo: ESP aste #atrAst
		| ESP NOM'.'NOM atributo ?  #atrTab;
		 
postInfo: condicion | inner ;

inner: (ESP 'INNER JOIN' ESP tabla ESP 'ON' ESP tabla'.'NOM ESP operador ESP tabla'.'NOM)? ;

SELECT: 'SELECT';

NOM: [A-Z]+;


NUM: [0-9]+;

ESP: ' '; 

from: 'FROM';

aste: '*';

tabla: ESP NOM tabla? #nomTabla
	  /*|ESP NOM tabla ?  #recurTab*/;

condicion: (ESP where ESP tabla'.'NOM ESP operador ESP comparador masCondi )?  #conCondicion;

where: 'WHERE';

operador: '=' | '<>' | '>' | '>=' | '<' | '<=';

comparador: NOM | NUM;

masCondi: (ESP 'AND'ESP tabla'.'NOM ESP operador ESP comparador masCondi) ? #condicionUnica
		  ;

fin:EOF   #finConsulta;



//: 'Int' | 'Double' | 'Float';


 /*
 consulta: SELECT ESP atributos EOF;// ESP FROM ESP NOMTAB EOF;

 //atributos: ASTER | NOMATR;
 atributos:atributos atributo
		  | atributo;

 atributo: ASTER 
		 | NOMATR ;*/

 

/*
 * Lexer Rules
 */

 /*
SELECT: 'SELECT';

NOMATR: [A-Z]+;

ESP: ' '; 

ASTER: '*';

FROM: 'FROM';

NOMTAB: [A-Z]+;

FINL:['\r\n']+;*/

/*compileUnit
	:	EOF
	;*/


/*
WS
	:	' ' -> channel(HIDDEN)
	;
*/